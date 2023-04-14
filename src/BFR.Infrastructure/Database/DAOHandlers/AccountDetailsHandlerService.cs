using BFR.Core.Attributes;
using BFR.Core.Interfaces;
using BFR.Infrastructure.Mapping;
using Microsoft.Extensions.Logging;

namespace BFR.Infrastructure.Database.DAOHandlers;

[ScopedService("AccountDetailsHandlerService")]
public class AccountDetailsHandlerService
{
	private const string KeyPrefix = "ACCOUNT_DETAILS_";
	private readonly ICacheManager _cacheManager;
	private readonly BFRContext _context;

	private readonly ILogger<AccountDetailsHandlerService> _logger;
	private readonly MapperlyHandler _mapper;

	public AccountDetailsHandlerService(ILogger<AccountDetailsHandlerService> logger, BFRContext context, ICacheManager cacheManager, MapperlyHandler mapper)
	{
		_logger = logger;
		_context = context;
		_cacheManager = cacheManager;
		_mapper = mapper;
	}
}
