using System.Buffers.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BFR.API.Intents;
using BFR.Core.DTO;
using BFR.Infrastructure.Database.DAOHandlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BFR.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly ILogger<AccountController> _logger;
	private readonly AccountHandlerService _accountHandler;
	private readonly IConfiguration _configuration;

	public AuthController(ILogger<AccountController> logger, AccountHandlerService accountHandler, IConfiguration configuration)
	{
		_logger = logger;
		_accountHandler = accountHandler;
		_configuration = configuration;
	}

	[HttpPost("Register")]
	public async Task<ActionResult> PostRegister(UserData intent)
	{
		try
		{
			var account = new AccountDto { Username = intent.Name, Password = intent.Password };

			if (await _accountHandler.CreateAccount(account))
				return Ok("Account created");

			return BadRequest("Account couldn't be created");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "{error}", ex.Message);
			return BadRequest(ex.Message);
		}
	}

	[HttpPost("LogIn")]
	public async Task<ActionResult> LogIn(UserData user)
	{
		var account = await _accountHandler.WithCredentials(new AccountDto { Username = user.Name, Password = user.Password });
		if ( account == null )
			return NotFound(user);
		return Ok(CreateToken(account));
	}

	private string CreateToken(AccountDto account)
	{
		try
		{
			var claims = new List<Claim> { new(ClaimTypes.Name, account.Username) };
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Authentication:Token")!));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
			var token = new JwtSecurityToken(claims: claims, expires: DateTime.Today.AddDays(1), signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
		catch ( Exception ex )
		{
			_logger.LogError(ex, "{error}", ex.Message);
			return string.Empty;
		}
	}
}