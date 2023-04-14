using Microsoft.EntityFrameworkCore;

namespace BFR.Core.Interfaces;

public interface IEntity
{
	public int Id { get; }

	public void OnModelCreating(ModelBuilder modeuilder);
}
