using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BFR.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BFR.Database.Entities;

public class Account : IEntity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public long Id { get; private set; }

	[Required]
	public string Username { get; set; } = null!;

	[Required]
	[PasswordPropertyText]
	public string Password { get; set; } = null!;

	[Required]
	public byte[] AuthenticationSecret { get; set; } = null!;

	public virtual AccountDetails? AccoundDetails { get; set; }

	void IEntity.OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Account>().Property(a => a.Username).HasMaxLength(255);
		modelBuilder.Entity<Account>().HasIndex(e => e.Username).IsUnique();

		modelBuilder.Entity<Account>().Property(a => a.Password).HasMaxLength(255);
		modelBuilder.Entity<Account>().HasIndex(e => e.Password).IsUnique();

		modelBuilder.Entity<Account>().Property(a => a.AuthenticationSecret);

		modelBuilder.Entity<Account>().HasOne(a => a.AccoundDetails).WithOne(a => a.Account).OnDelete(DeleteBehavior.Cascade);
	}
}
