using BFR.Shared.DTO;
using Mapster;

namespace BFR.Mapping.DatabaseConfigs;

public class Account : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.ForType<Account, AccountDto>();
	}
}
