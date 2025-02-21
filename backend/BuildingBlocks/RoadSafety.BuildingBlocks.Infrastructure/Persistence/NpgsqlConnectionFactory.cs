using System.Data.Common;
using Npgsql;
using RoadSafety.BuildingBlocks.QueryStack.Persistance;

namespace RoadSafety.BuildingBlocks.Infrastructure.Persistence
{
	public class NpgsqlConnectionFactory(NpgsqlDataSource dataSource) : IConnectionFactory
	{
		public async Task<DbConnection> OpenConnectionAsync(CancellationToken cancellationToken) =>
			await dataSource.OpenConnectionAsync(cancellationToken);
	}
}
