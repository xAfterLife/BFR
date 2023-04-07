using System.Reflection;
using BFR.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BFR.Database;

public static class DatabaseExtensions
{
	public static void LoadDataModels(this ModelBuilder modelBuilder)
	{
		var dataModelTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IEntity).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

		foreach ( var type in dataModelTypes )
		{
			var dataModel = (IEntity?)Activator.CreateInstance(type);
			dataModel?.OnModelCreating(modelBuilder);
		}
	}
}
