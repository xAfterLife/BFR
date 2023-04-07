using System.Reflection;
using BFR.Shared.Attributes;
using Mapster;

namespace BFR.Mapping;

public static class MappingExtensions
{
	public static void ConfigureMapping()
	{
		var config = TypeAdapterConfig.GlobalSettings;
		var mappingTypes = typeof(IRegister).Assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<MappingConfigAttribute>() != null);

		foreach ( var type in mappingTypes )
		{
			var instance = (IRegister?)Activator.CreateInstance(type);
			instance?.Register(config);
		}
	}
}
