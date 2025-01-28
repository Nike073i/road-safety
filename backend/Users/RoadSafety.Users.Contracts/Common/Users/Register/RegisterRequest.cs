namespace RoadSafety.Users.Contracts.Common.Users.Register
{
	public record RegisterRequest(
		string FirstName,
		string LastName,
		string UserName,
		string Email,
		string Password
	);
}
