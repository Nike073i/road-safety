namespace RoadSafety.Users.QueryStack.Users.GetUserPermissions
{
	public interface IGetUserPermissionsDataSource
	{
		Task<UserPermissionsViewModel?> GetData(
			Guid UserId,
			CancellationToken cancellationToken = default
		);
	}
}
