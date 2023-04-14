using BFR.Core.DTO;
using BFR.Infrastructure.Database.DAOHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BFR.API.Controllers;

[ApiController, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
	private readonly ILogger<AccountController> _logger;
	private readonly AccountHandlerService _accountHandler;

	public AccountController(ILogger<AccountController> logger, AccountHandlerService accountHandler)
	{
		_logger = logger;
		_accountHandler = accountHandler;
	}

	[HttpGet("GetAccount")]
	public async Task<ActionResult<AccountDto>> GetAccount(int id)
	{
		var account = await _accountHandler.GetAccount(id);

		if ( account == null )
			return NotFound();

		return Ok(account);
	}

	[HttpGet("GetAllAccounts")]
	public async Task<ActionResult<List<AccountDto>>> GetAllAccounts()
	{
		var accounts = await _accountHandler.GetAllAccounts();
		return Ok(accounts.ToList());
	}
}