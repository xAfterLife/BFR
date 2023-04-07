using BFR.Shared.DTO;
using Mapster;

namespace BFR.Mapping.DatabaseConfigs;

internal class AccountDetails : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.ForType<AccountDetails, AccountDetailsDto>();
	}
}
