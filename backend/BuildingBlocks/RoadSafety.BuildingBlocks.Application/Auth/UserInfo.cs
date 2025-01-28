namespace RoadSafety.BuildingBlocks.Application.Auth
{
	public record UserInfo(Guid UserId, string Username, string Email, bool EmailVerified);
}
