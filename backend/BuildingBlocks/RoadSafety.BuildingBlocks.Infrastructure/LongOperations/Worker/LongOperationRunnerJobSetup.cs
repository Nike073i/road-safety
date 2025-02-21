using Microsoft.Extensions.Options;
using Quartz;

namespace RoadSafety.BuildingBlocks.Infrastructure.LongOperations.Worker
{
	public class LongOperationRunnerJobSetup<TJob>(LongOperationRunnerOptions runnerOptions)
		: IConfigureOptions<QuartzOptions>
		where TJob : LongOperationRunnerJob
	{
		private const string _jobName = nameof(LongOperationRunnerJob);

		public void Configure(QuartzOptions options) =>
			options
				.AddJob<TJob>(configure => configure.WithIdentity(_jobName))
				.AddTrigger(configure =>
					configure
						.ForJob(_jobName)
						.WithIdentity(_jobName + "_trigger")
						.StartNow()
						.WithSimpleSchedule(schedule =>
							schedule
								.WithIntervalInSeconds(runnerOptions.IntervalInSeconds)
								.RepeatForever()
						)
				);
	}
}
