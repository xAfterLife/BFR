using BFR.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BFR.Infrastructure.Database.Queries;

public static class AccountQueries
{
	/// <summary>
	///     Gets an account from the database that matches the specified credentials.
	/// </summary>
	/// <returns>An <see cref="Account" /> object that matches the specified credentials, or <c>null</c> if no match is found.</returns>
	public static readonly Func<BFRContext, string, string, Task<Account?>> WithCredentialsAsync = EF.CompileAsyncQuery((BFRContext context, string username, string password) => context.Accounts.FirstOrDefault(x => x.Username == username && x.Password == password));

	/// <summary>
	///     Gets an account from the database with the specified Username.
	/// </summary>
	/// <returns>An <see cref="Account" /> object that matches the specified credentials, or <c>null</c> if no match is found.</returns>
	public static readonly Func<BFRContext, string, Task<Account?>> FromUsername = EF.CompileAsyncQuery((BFRContext context, string username) => context.Accounts.FirstOrDefault(x => x.Username == username));

	/// <summary>
	///     Gets an account from the database with the specified ID.
	/// </summary>
	/// <returns>An <see cref="Account" /> object that matches the specified credentials, or <c>null</c> if no match is found.</returns>
	public static readonly Func<BFRContext, int, Task<Account?>> FromId = EF.CompileAsyncQuery((BFRContext context, int id) => context.Accounts.FirstOrDefault(x => x.Id == id));
}
