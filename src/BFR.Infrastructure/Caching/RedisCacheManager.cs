using System.Text;
using System.Text.Json.Serialization;
using BFR.Core.Interfaces;
using BFR.Core.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace BFR.Infrastructure.Caching;

public sealed class RedisCacheManager : ICacheManager
{
	private readonly IDistributedCache _cache;

	private readonly DistributedCacheEntryOptions _cacheOptions;
	private readonly JsonService _jsonService;
	private readonly ILogger<RedisCacheManager> _logger;

	public RedisCacheManager(IDistributedCache cache, ILogger<RedisCacheManager> logger, JsonService jsonService)
	{
		_cache = cache;
		_logger = logger;
		_jsonService = jsonService;

		_cacheOptions = new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(15) };
	}

	public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
	{
		try
		{
			var cachedValue = await _cache.GetStringAsync(key, cancellationToken);
			if ( cachedValue == null )
				return default;

			var retValue = _jsonService.Deserialize<T>(cachedValue);
			return retValue ?? default;
		}
		catch ( Exception ex )
		{
			_logger.LogError(ex, "{error message}", ex.Message);
			return default;
		}
	}

	public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
	{
		try
		{
			var serializedValue =  _jsonService.Serialize(value);
			if ( serializedValue != null )
				await _cache.SetStringAsync(key, serializedValue, _cacheOptions, cancellationToken);
		}
		catch ( Exception ex )
		{
			_logger.LogError(ex, "{error message}", ex.Message);
		}
	}
}
