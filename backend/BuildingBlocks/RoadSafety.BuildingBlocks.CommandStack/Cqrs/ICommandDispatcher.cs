using ErrorOr;

namespace RoadSafety.BuildingBlocks.CommandStack.Cqrs
{
	public interface ICommandDispatcher
	{
		Task<ErrorOr<Success>> SendCommand(
			ICommand command,
			CancellationToken cancellationToken = default
		);

		Task<ErrorOr<TResult>> SendCommand<TResult>(
			ICommand<TResult> command,
			CancellationToken cancellationToken = default
		);
	}
}
