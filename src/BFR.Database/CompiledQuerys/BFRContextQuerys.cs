using BFR.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace BFR.Database.CompiledQuerys;

// ReSharper disable once InconsistentNaming
public static class BFRContextQuerys
{
	/// <summary>
	///     Gets an account from the database that matches the specified credentials.
	/// </summary>
	/// <returns>An <see cref="Account" /> object that matches the specified credentials, or <c>null</c> if no match is found.</returns>
	public static readonly Func<BFRContext, string, string, Task<Account?>> GetAccountWithCredentialsAsync = EF.CompileAsyncQuery((BFRContext context, string username, string password) => context.Accounts.FirstOrDefault(x => x.Username == username && x.Password == password));
}
