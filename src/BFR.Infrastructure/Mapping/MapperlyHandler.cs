using BFR.Core.Attributes;
using BFR.Core.DTO;
using BFR.Core.Entities;
using Riok.Mapperly.Abstractions;

namespace BFR.Infrastructure.Mapping;

[Mapper]
[ScopedService("MapperlyHandler")]
public partial class MapperlyHandler
{
	public partial Account Map(AccountDto accountDto);
	public partial AccountDto Map(Account account);
}
