using System.Data;

namespace RoadSafety.BuildingBlocks.QueryStack.Persistance
{
	public interface IConnectionFactory
	{
		IDbConnection CreateConnection(bool open = true);
	}
}
