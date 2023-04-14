using BFR.Core.Attributes;
using BFR.Core.DTO;
using BFR.Core.Entities;
using BFR.Core.Interfaces;
using BFR.Infrastructure.Database.Queries;
using BFR.Infrastructure.Mapping;
using Microsoft.Extensions.Logging;

namespace BFR.Infrastructure.Database.DAOHandlers;

[ScopedService("AccountHandlerService")]
public class AccountHandlerService
{
	private const string KeyPrefix = "ACCOUNT_";
	private readonly ICacheManager _cacheManager;
	private readonly BFRContext _context;

	private readonly ILogger<AccountHandlerService> _logger;
	private readonly MapperlyHandler _mapper;

	public AccountHandlerService(ILogger<AccountHandlerService> logger, BFRContext context, ICacheManager cacheManager, MapperlyHandler mapper)
	{
		_logger = logger;
		_context = context;
		_cacheManager = cacheManager;
		_mapper = mapper;
	}

	public ValueTask<IEnumerable<AccountDto>> GetAllAccounts(CancellationToken token = default)
	{
		var accounts = _context.Accounts;
		return ValueTask.FromResult(accounts.Select(_mapper.Map));
	}

	public async ValueTask<AccountDto?> GetAccount(int id, CancellationToken token = default)
	{
		var cachingKey = $"{KeyPrefix}{id}";

		var cachedAccount = await _cacheManager.GetAsync<AccountDto>(cachingKey, token);
		if ( cachedAccount != null )
			return cachedAccount;

		var account = await AccountQueries.FromId(_context, id);

		if ( account == null )
			return null;

		var accountDto = _mapper.Map(account);
		await _cacheManager.SetAsync(cachingKey, accountDto, token);
		return accountDto;
	}

	public async ValueTask<AccountDto?> WithCredentials(AccountDto account, CancellationToken token = default)
	{
		var acc = await AccountQueries.WithCredentialsAsync(_context, account.Username, account.Password);
		return acc == null ? null : _mapper.Map(acc);
	}

	public async ValueTask<bool> CreateAccount(AccountDto account, CancellationToken token = default)
	{
		if ( await AccountQueries.FromUsername(_context, account.Username) != null )
			return false;

		await _context.Accounts.AddAsync(_mapper.Map(account), token);
		var changeCount = await _context.SaveChangesAsync(token);
		return changeCount != 0;
	}
}
