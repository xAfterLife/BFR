using BFR.Shared.Interfaces;

namespace BFR.Shared.DTO;

public class AccountDto : IDto
{
	public string Username { get; set; } = null!;
	public string Password { get; set; } = null!;
	public byte[] AuthenticationSecret { get; set; } = null!;

	public AccountDto(int id)
	{
		Id = id;
	}

	public long Id { get; }
}
