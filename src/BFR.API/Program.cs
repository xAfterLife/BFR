using System.Text;
using BFR.Core.Attributes;
using BFR.Core.Interfaces;
using BFR.Core.Services.Static;
using BFR.Infrastructure.Caching;
using BFR.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace BFR.API;

public class Program
{
	public static void Main(string[] args)
	{
		var app = GetWebApplication(args);
		ConfigureApplication(app);

		app.Run();
	}

	private static WebApplication GetWebApplication(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		AddServices(builder.Services);

		return builder.Build();
	}

	private static void AddServices(IServiceCollection services)
	{
		var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

		services.AddSingleton<IConfiguration>(config);
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddAuthentication().AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true, 
				ValidateAudience = false, 
				ValidateIssuer = false, 
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("Authentication:Token")!))
			};
		});
		services.AddSwaggerGen(options =>
		{
			options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey
			});

			options.OperationFilter<SecurityRequirementsOperationFilter>();
		});
		services.AddLogging(builder =>
		{
			builder.AddConsole();
		});
		services.AddDbContext<BFRContext>(options =>
		{
			options.UseNpgsql(config.GetConnectionString("BFRContext"));
		});
		services.AddStackExchangeRedisCache(options =>
		{
			options.Configuration = config.GetConnectionString("BFRCache");
			options.InstanceName = "BFRCache";
		});

		services.AddSingleton<ICacheManager, RedisCacheManager>();
		foreach (var type in StaticAssemblyService.GetFromAttribute<SingletonServiceAttribute>())
			services.AddSingleton(type);
		foreach (var type in StaticAssemblyService.GetFromAttribute<ScopedServiceAttribute>())
			services.AddScoped(type);
	}

	private static void ConfigureApplication(WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();
		app.UseAuthorization();
		app.UseAuthentication();
		app.MapControllers();
	}
}
