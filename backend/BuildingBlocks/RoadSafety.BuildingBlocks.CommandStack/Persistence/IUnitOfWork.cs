using ErrorOr;

namespace RoadSafety.BuildingBlocks.CommandStack.Persistence
{
	public interface IUnitOfWork
	{
		Task<ErrorOr<Success>> CommitAsync(CancellationToken cancellationToken = default);
	}
}
