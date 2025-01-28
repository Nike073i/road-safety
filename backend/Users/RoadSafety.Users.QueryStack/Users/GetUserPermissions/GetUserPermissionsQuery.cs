using RoadSafety.BuildingBlocks.QueryStack.Cache;

namespace RoadSafety.Users.QueryStack.Users.GetUserPermissions
{
	public record GetUserPermissionsQuery(Guid UserId) : CacheableQuery<UserPermissionsViewModel?>;
}
