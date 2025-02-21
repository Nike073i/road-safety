using System.Data.Common;

namespace RoadSafety.BuildingBlocks.QueryStack.Persistance
{
	public interface IConnectionFactory
	{
		Task<DbConnection> OpenConnectionAsync(CancellationToken cancellationToken = default);
	}
}
