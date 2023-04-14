using System.Diagnostics;

namespace BFR.Core.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class SingletonServiceAttribute : Attribute
{
	public string Name { get; }

	public SingletonServiceAttribute(string name)
	{
		Name = name;
		Debug.WriteLine($"{Name} constructed");
	}
}
