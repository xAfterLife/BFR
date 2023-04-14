using BFR.Core.Entities;
using BFR.Core.Interfaces;

namespace BFR.Core.DTO;

public class AccountDetailsDto : IDto
{
	public int AccountId { get; set; }

	public virtual Account? Account { get; set; }

	public int Level { get; set; }

	public int CurrentStamina { get; set; }

	public int MaxStamina { get; set; }

	public int Diamonds { get; set; }

	public long Gold { get; set; }

	public int Id { get; set; }
}
