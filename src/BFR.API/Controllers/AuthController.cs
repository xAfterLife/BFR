using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BFR.API.Intent;
using BFR.Database;
using BFR.Database.CompiledQuerys;
using BFR.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BFR.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
	private readonly BFRContext _context;
	private readonly ILogger<AuthController> _logger;

	public AuthController(ILogger<AuthController> logger, BFRContext context)
	{
		_logger = logger;
		_context = context;
	}

	[HttpPost("Login", Name = "PostLogin")]
	public async Task<IActionResult> PostLogin(LoginIntent authIntent)
	{
		var account = await BFRContextQuerys.GetAccountWithCredentialsAsync(_context, authIntent.Username, authIntent.Password);
		if (account == null)
			return BadRequest("Username or Password not matching");

		var claims = new List<Claim> { new(ClaimTypes.Name, account.Username) };
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmnopqrstuvwxyz"));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var expires = DateTime.Now.AddMinutes(5);

		var token = new JwtSecurityToken("Jwt:Issuer", "Jwt:Audience", claims, expires, signingCredentials: creds);
		return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = expires });
	}
}
