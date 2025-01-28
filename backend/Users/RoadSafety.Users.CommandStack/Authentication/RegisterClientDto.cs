namespace RoadSafety.Users.CommandStack.Authentication
{
	public record RegisterClientDto(
		string FirstName,
		string LastName,
		string UserName,
		string Email,
		string Password
	);
}
