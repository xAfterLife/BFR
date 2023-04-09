namespace BFR.API.Intent;

public class LoginIntent
{
	public string Username { get; set; }
	public string Password { get; set; }

	public LoginIntent(string username, string password)
	{
		Username = username;
		Password = password;
	}
}
