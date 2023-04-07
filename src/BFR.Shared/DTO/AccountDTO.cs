﻿using BFR.Shared.Interfaces;

namespace BFR.Shared.DTO;

public class AccountDto : IDto
{
	public long Id { get; }
	public string Username { get; set; } = null!;
	public string Password { get; set; } = null!;
	public byte[] AuthenticationSecret { get; set; } = null!;
}
