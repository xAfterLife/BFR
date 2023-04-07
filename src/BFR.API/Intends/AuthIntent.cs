namespace BFR.API.Intends;

public class AuthIntent
{
	public string Username { get; set; }
	public string Password { get; set; }

	public AuthIntent(string username, string password)
	{
		Username = username;
		Password = password;
	}
}
