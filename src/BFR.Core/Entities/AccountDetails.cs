using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BFR.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BFR.Core.Entities;

public class AccountDetails : IEntity
{
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

	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; private set; }

	public void OnModelCreating(ModelBuilder modelBuilder)
	{
		var builder = modelBuilder.Entity<AccountDetails>();

		builder.Property(a => a.Level);
		builder.Property(a => a.CurrentStamina);
		builder.Property(a => a.MaxStamina);
		builder.Property(a => a.Diamonds);
		builder.Property(a => a.Gold);
	}
}
