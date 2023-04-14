using BFR.Core.Entities;
using BFR.Core.Interfaces;

namespace BFR.Core.DTO;

public class AccountDto : IDto
{
	public string Username { get; set; } = null!;
	public string Password { get; set; } = null!;
	public byte[] AuthenticationSecret { get; set; } = null!;
	public virtual AccountDetails? AccoundDetails { get; set; }
	public int Id { get; set; }
}
