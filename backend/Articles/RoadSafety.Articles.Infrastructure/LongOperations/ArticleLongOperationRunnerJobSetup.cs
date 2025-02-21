using RoadSafety.BuildingBlocks.Infrastructure.LongOperations.Worker;

namespace RoadSafety.Articles.Infrastructure.LongOperations
{
	public class ArticleLongOperationRunnerJobSetup(LongOperationRunnerOptions runnerOptions)
		: LongOperationRunnerJobSetup<ArticleLongOperationRunnerJob>(runnerOptions);
}
