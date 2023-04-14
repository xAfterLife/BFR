using BFR.Core.DTO;
using BFR.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

namespace BFR.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerInfoController : ControllerBase
{
	private readonly BFRContext _context;
	private readonly ILogger<PlayerInfoController> _logger;

	public PlayerInfoController(ILogger<PlayerInfoController> logger, BFRContext context)
	{
		_logger = logger;
		_context = context;
	}

	[HttpGet(Name = "GetRandomInfo")]
	public IActionResult GetRandomInfo()
	{
		var guid = Guid.NewGuid();
		var rand = new Random(guid.GetHashCode());

		var accountDetails = new AccountDetailsDto
		{
			AccountId = rand.Next(1, 1000),
			CurrentStamina = rand.Next(10, 50),
			Diamonds = rand.Next(0, 500),
			Gold = rand.Next(100, 100000),
			Level = rand.Next(1, 90),
			MaxStamina = 50
		};

		_logger.LogInformation("{accountDetails}", accountDetails);
		return Ok(accountDetails);
	}
}
