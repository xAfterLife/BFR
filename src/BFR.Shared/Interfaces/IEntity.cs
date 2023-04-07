using Microsoft.EntityFrameworkCore;

namespace BFR.Shared.Interfaces;

public interface IEntity
{
	public long Id { get; }

	public void OnModelCreating(ModelBuilder modelBuilder);
}
