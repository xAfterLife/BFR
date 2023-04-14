using BFR.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BFR.Infrastructure.Database.Queries;

public static class AccountDetailsQueries
{
	/// <summary>
	///     Gets all accounts
	/// </summary>
	/// ///
	/// <returns>An <see cref="List{T}" /> of <see cref="Account" />.</returns>
	public static readonly Func<BFRContext, Task<List<Account>>> All = EF.CompileAsyncQuery((BFRContext context) => context.Accounts.ToList());
}
