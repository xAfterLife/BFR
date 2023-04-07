namespace BFR.Shared.Enums;

public enum AuthState
{
	Ok = 0,
	OldClient = 1,
	UnhandledError = 2,
	Maintenance = 3,
	AlreadyConnected = 4,
	AccountOrPasswordWrong = 5,
	CantConnect = 6,
	Banned = 7
}
