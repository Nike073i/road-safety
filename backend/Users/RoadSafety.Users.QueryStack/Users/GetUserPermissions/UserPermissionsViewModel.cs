namespace RoadSafety.Users.QueryStack.Users.GetUserPermissions
{
	public class UserPermissionsViewModel
	{
		public required Guid UserId { get; init; }
		public required IReadOnlyCollection<PermissionViewModel> Permissions { get; init; }

		public class PermissionViewModel
		{
			public required int Code { get; init; }
			public required string Name { get; init; }
		}
	}
}
