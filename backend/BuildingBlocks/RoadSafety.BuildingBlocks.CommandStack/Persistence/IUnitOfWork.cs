using System.Data.Common;
using ErrorOr;

namespace RoadSafety.BuildingBlocks.CommandStack.Persistence
{
	public interface IUnitOfWork
	{
		DbTransaction? Transaction { get; }
		DbConnection Connection { get; }
		Task<DbTransaction> StartTransactionAsync(CancellationToken cancellationToken = default);
		Task<ErrorOr<Success>> CommitAsync(CancellationToken cancellationToken = default);
		bool IsTransactionStarted();
	}
}
