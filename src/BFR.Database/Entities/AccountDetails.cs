using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BFR.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BFR.Database.Entities;

public class AccountDetails : IEntity
{
	[ForeignKey(nameof(Account))]
	public long AccountId { get; set; }

	public virtual Account? Account { get; set; }

	[Required]
	public int Level { get; set; }

	[Required]
	public int CurrentStamina { get; set; }

	[Required]
	public int MaxStamina { get; set; }

	[Required]
	public int Diamonds { get; set; }

	[Required]
	public long Gold { get; set; }

	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public long Id { get; private set; }

	public void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<AccountDetails>().Property(a => a.Level);
		modelBuilder.Entity<AccountDetails>().Property(a => a.CurrentStamina);
		modelBuilder.Entity<AccountDetails>().Property(a => a.MaxStamina);
		modelBuilder.Entity<AccountDetails>().Property(a => a.Diamonds);
		modelBuilder.Entity<AccountDetails>().Property(a => a.Gold);
	}
}
