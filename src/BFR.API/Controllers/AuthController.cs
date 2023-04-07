using BFR.API.Intends;
using BFR.Database;
using BFR.Database.CompiledQuerys;
using Microsoft.AspNetCore.Mvc;

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

	[HttpPost("Auth", Name = "PostAuth")]
	public async Task<IActionResult> PostAuth(AuthIntent authIntent)
	{
		var account = await BFRContextQuerys.GetAccountWithCredentialsAsync(_context, authIntent.Username, authIntent.Password);
		if ( account == null )
			return BadRequest("Username or Password not matching");

		return Ok(Convert.ToBase64String(account.AuthenticationSecret));
	}
}
