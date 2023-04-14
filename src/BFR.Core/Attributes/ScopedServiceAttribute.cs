using System.Diagnostics;

namespace BFR.Core.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ScopedServiceAttribute : Attribute
{
	public string Name { get; }

	public ScopedServiceAttribute(string name)
	{
		Name = name;
		Debug.WriteLine($"{Name} constructed");
	}
}
