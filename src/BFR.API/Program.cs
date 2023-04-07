using BFR.Database;
using BFR.Mapping;

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
	}
}
