using RoadSafety.BuildingBlocks.Application.Clock;
using RoadSafety.BuildingBlocks.CommandStack.LongOperations;
using RoadSafety.BuildingBlocks.CommandStack.Persistence;
using RoadSafety.BuildingBlocks.Infrastructure.LongOperations.Worker;
using RoadSafety.BuildingBlocks.Infrastructure.Serialization;

namespace RoadSafety.Articles.Infrastructure.LongOperations
{
	public class ArticleLongOperationRunnerJob(
		IUnitOfWork unitOfWork,
		IDateTimeProvider dateTimeProvider,
		ISerializer serializer,
		ILongOperationDispatcher operationDispatcher
	) : LongOperationRunnerJob(unitOfWork, dateTimeProvider, serializer, operationDispatcher)
	{
		protected override string Schema => "articles";
	}
}
