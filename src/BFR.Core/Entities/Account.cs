using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BFR.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BFR.Core.Entities;

public class Account : IEntity
{
	[Required]
	public string Username { get; set; } = null!;

	[Required]
	[PasswordPropertyText]
	public string Password { get; set; } = null!;

	public byte[]? AuthenticationSecret { get; set; } = null!;

	public virtual AccountDetails? AccoundDetails { get; set; }

	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; private set; }

	public void OnModelCreating(ModelBuilder modelBuilder)
	{
		var builder = modelBuilder.Entity<Account>();

		builder.Property(a => a.Username).HasMaxLength(255);
		builder.HasIndex(e => e.Username).IsUnique();

		builder.Property(a => a.Password).HasMaxLength(255);
		builder.HasIndex(e => e.Password).IsUnique();

		builder.Property(a => a.AuthenticationSecret);

		builder.HasOne(a => a.AccoundDetails).WithOne(a => a.Account).OnDelete(DeleteBehavior.Cascade);
	}
}
