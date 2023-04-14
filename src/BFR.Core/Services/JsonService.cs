using System.Text.Json;
using BFR.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace BFR.Core.Services;

[SingletonService("JsonService")]
public sealed class JsonService
{
	private readonly ILogger<JsonService> _logger;
	private readonly JsonSerializerOptions _options;

	public JsonService(ILogger<JsonService> logger)
	{
		_logger = logger;
		_options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
	}

	public JsonService(ILogger<JsonService> logger, JsonSerializerOptions options)
	{
		_logger = logger;
		_options = options;
	}

	public T? Deserialize<T>(string data)
	{
		try
		{
			return JsonSerializer.Deserialize<T>(data, _options);
		}
		catch ( JsonException ex )
		{
			_logger.LogError(ex, "Error deserializing JSON data");
			return default;
		}
	}

	public string? Serialize<T>(T value)
	{
		try
		{
			return JsonSerializer.Serialize(value, _options);
		}
		catch ( JsonException ex )
		{
			_logger.LogError(ex, "Error serializing object to JSON");
			return default;
		}
	}
}
