using Dapper;
using RoadSafety.BuildingBlocks.QueryStack.Persistance;
using RoadSafety.Users.QueryStack.Users.GetUserPermissions;

namespace RoadSafety.Users.Infrastructure.Users.DataSources
{
	internal class GetUserPermissionsDataSource(IConnectionFactory connectionFactory)
		: IGetUserPermissionsDataSource
	{
		private readonly IConnectionFactory _connectionFactory = connectionFactory;
		private const string _sql = """
			SELECT p.code, p.name
				FROM (
					SELECT role_code 
					FROM users.user_roles 
					WHERE user_id = @UserId
					) as r
				LEFT JOIN users.role_permissions AS rp
					ON r.role_code = rp.role_code
				LEFT JOIN users.permissions AS p
					ON rp.permission_code = p.code;
			""";

		public async Task<UserPermissionsViewModel?> GetData(
			Guid UserId,
			CancellationToken cancellationToken
		)
		{
			using var connection = _connectionFactory.CreateConnection();
			var data = await connection.QueryAsync<Data>(_sql, param: new { UserId });
			if (!data.Any())
				return default;
			var viewModel = new UserPermissionsViewModel
			{
				UserId = UserId,
				Permissions = data.Where(e => e.Code.HasValue)
					.Select(e => new UserPermissionsViewModel.PermissionViewModel
					{
						Code = e.Code!.Value,
						Name = e.Name!,
					})
					.ToList(),
			};
			return viewModel;
		}

		private class Data
		{
			public int? Code { get; set; }
			public string? Name { get; set; }
		}
	}
}
