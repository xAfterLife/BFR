using System.Text;
using BFR.Database;
using BFR.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BFR.API;

public class Program
{
	public static void Main(string[] args)
	{
		var app = GetWebApplication(args);

		if ( app.Environment.IsDevelopment() )
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();
		app.UseAuthorization();
		app.UseAuthentication();
		app.MapControllers();
		app.Run();
	}

	private static WebApplication GetWebApplication(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		AddServices(builder.Services);
		MappingExtensions.ConfigureMapping();

		return builder.Build();
	}

	private static void AddServices(IServiceCollection services)
	{
		var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

		services.AddSingleton<IConfiguration>(config);
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		services.AddLogging(builder =>
		{
			builder.AddConsole();
		});
		services.AddDbContext<BFRContext>();
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
		{
			options.RequireHttpsMetadata = false;
			options.SaveToken = true;
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = "Jwt:Issuer",
				ValidAudience = "Jwt:Audience",
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmnopqrstuvwxyz")),
				ClockSkew = TimeSpan.Zero
			};
		});
	}
}
