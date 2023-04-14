namespace BFR.Core.Enums;

public enum AuthState
{
	Ok = 0,
	OldClient = 1,
	UnhandledError = 2,
	Maintenance = 3,
	AlreadyConnected = 4,
	AccountOrPasswordWrong = 5,
	Banned = 6
}
