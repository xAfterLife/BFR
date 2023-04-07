using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BFR.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BFR.Database.Entities;

public class AccountDetails : IEntity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public long Id { get; private set; }

	[ForeignKey(nameof(Account))]
	public int AccountId { get; set; }

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

	void IEntity.OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<AccountDetails>().Property(a => a.Level);
		modelBuilder.Entity<AccountDetails>().Property(a => a.CurrentStamina);
		modelBuilder.Entity<AccountDetails>().Property(a => a.MaxStamina);
		modelBuilder.Entity<AccountDetails>().Property(a => a.Diamonds);
		modelBuilder.Entity<AccountDetails>().Property(a => a.Gold);
	}
}
