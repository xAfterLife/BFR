using BFR.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BFR.Infrastructure.Database;

public sealed class BFRContext : DbContext
{
	public DbSet<Account> Accounts { get; set; } = null!;
	public DbSet<AccountDetails> AccountDetails { get; set; } = null!;

	public BFRContext() { }
	public BFRContext(DbContextOptions<BFRContext> options) : base(options) {}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (optionsBuilder.IsConfigured)
			return;

		optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BFR;Username=postgres;Password=docker");
	}
}
