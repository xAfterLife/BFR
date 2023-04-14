using BFR.Core.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OtpNet;

namespace BFR.Core.Services;

[SingletonService("TotpService")]
public sealed class TotpService
{
	private readonly ILogger<TotpService> _logger;
	private readonly OtpHashMode _mode;
	private readonly int _size;
	private readonly int _step;

	public TotpService(ILogger<TotpService> logger, IConfiguration configuration)
	{
		_logger = logger;
		_mode = configuration.GetValue<OtpHashMode>("OtpNet:Mode");
		_size = configuration.GetValue<int>("OtpNet:Size");
		_step = configuration.GetValue<int>("OtpNet:Step");
	}

	public byte[]? GenerateAuthCode(int length = 32)
	{
		try
		{
			return KeyGeneration.GenerateRandomKey(length);
		}
		catch ( Exception ex )
		{
			_logger.LogError(ex, "{error}", ex.Message);
			return default;
		}
	}

	public bool VerifyCode(byte[] secret, string code, DateTime? timeStamp = null)
	{
		try
		{
			var totp = new Totp(secret, _step, _mode, _size);
			return timeStamp.HasValue ? totp.VerifyTotp(timeStamp.Value, code, out _) : totp.VerifyTotp(code, out _);
		}
		catch ( Exception ex )
		{
			_logger.LogError(ex, "{error}", ex.Message);
			return false;
		}
	}
}
