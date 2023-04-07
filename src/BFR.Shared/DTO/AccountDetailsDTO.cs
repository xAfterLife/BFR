using BFR.Shared.Interfaces;

namespace BFR.Shared.DTO;

public class AccountDetailsDto : IDto
{
	public long Id { get; }
	public int AccountId { get; set; }
	public int Level { get; set; }
	public int CurrentStamina { get; set; }
	public int MaxStamina { get; set; }
	public int Diamonds { get; set; }
	public long Gold { get; set; }
}
