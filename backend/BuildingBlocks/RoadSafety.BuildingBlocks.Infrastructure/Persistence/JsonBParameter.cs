using System.Data;
using Npgsql;
using NpgsqlTypes;
using static Dapper.SqlMapper;

namespace RoadSafety.BuildingBlocks.Infrastructure.Persistence
{
	public class JsonBParameter(string json) : ICustomQueryParameter
	{
		public void AddParameter(IDbCommand command, string name)
		{
			var parameter = (NpgsqlParameter)command.CreateParameter();
			parameter.ParameterName = name;
			parameter.Value = json;
			parameter.NpgsqlDbType = NpgsqlDbType.Jsonb;
			command.Parameters.Add(parameter);
		}
	}
}
