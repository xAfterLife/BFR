using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BFR.Database;

// ReSharper disable once InconsistentNaming
public class BFRContext : DbContext
{
	private readonly IConfiguration _configuration;

	public BFRContext(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql(_configuration.GetConnectionString("MyDatabase"));
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.LoadDataModels();
	}
}
