using System.Reflection;

namespace BFR.Core.Services.Static;

public static class StaticAssemblyService
{
	public static IEnumerable<Type> GetFromInterface<T>()
	{
		var types = new List<Type>();

		foreach ( var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location)) )
			types.AddRange(assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsInterface && typeof(T).IsAssignableFrom(t)));

		return types;
	}

	public static IEnumerable<Type> GetFromAttribute<T>() where T : Attribute
	{
		var types = new List<Type>();

		foreach ( var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location)) )
			types.AddRange(assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsInterface && t.GetCustomAttribute<T>() != null));

		return types;
	}
}
