namespace RoadSafety.BuildingBlocks.CommandStack.Cqrs
{
	public interface ICommandDispatcher
	{
		Task SendCommand<TCommand>(TCommand command, CancellationToken cancellationToken = default)
			where TCommand : ICommand;

		Task<TResult> SendCommand<TCommand, TResult>(
			ICommand<TResult> command,
			CancellationToken cancellationToken = default
		)
			where TCommand : ICommand<TResult>;
	}
}
