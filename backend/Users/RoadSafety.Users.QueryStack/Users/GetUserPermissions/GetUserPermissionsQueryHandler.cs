using RoadSafety.BuildingBlocks.QueryStack.Cqrs;

namespace RoadSafety.Users.QueryStack.Users.GetUserPermissions
{
	public class GetUserPermissionsQueryHandler(IGetUserPermissionsDataSource dataSource)
		: IQueryHandler<GetUserPermissionsQuery, UserPermissionsViewModel?>
	{
		private readonly IGetUserPermissionsDataSource _dataSource = dataSource;

		public Task<UserPermissionsViewModel?> Handle(
			GetUserPermissionsQuery request,
			CancellationToken cancellationToken
		) => _dataSource.GetData(request.UserId, cancellationToken);
	}
}
