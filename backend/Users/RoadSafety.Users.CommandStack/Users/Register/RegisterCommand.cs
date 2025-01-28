using RoadSafety.BuildingBlocks.CommandStack.Cqrs;

namespace RoadSafety.Users.CommandStack.Users.Register
{
	public record RegisterCommand(
		string FirstName,
		string LastName,
		string UserName,
		string Email,
		string Password
	) : ICommand<Guid>;
}
