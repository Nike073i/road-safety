using System.Data;
using Npgsql;
using RoadSafety.BuildingBlocks.QueryStack.Persistance;

namespace RoadSafety.BuildingBlocks.Infrastructure.Persistence
{
	public class NpgsqlConnectionFactory(DatabaseSettings databaseSettings) : IConnectionFactory
	{
		private readonly DatabaseSettings _databaseSettings = databaseSettings;

		public IDbConnection CreateConnection(bool open)
		{
			var connection = new NpgsqlConnection(_databaseSettings.СonnectionString);
			if (open)
				connection.Open();
			return connection;
		}
	}
}
