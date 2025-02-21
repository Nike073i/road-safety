using ErrorOr;
using MediatR;
using RoadSafety.BuildingBlocks.CommandStack.Cqrs;

namespace RoadSafety.BuildingBlocks.Infrastructure.Cqrs
{
	public class CommandDispatcher(ISender sender) : ICommandDispatcher
	{
		Task<ErrorOr<Success>> ICommandDispatcher.SendCommand(
			ICommand command,
			CancellationToken cancellationToken
		) => sender.Send(command, cancellationToken);

		Task<ErrorOr<TResult>> ICommandDispatcher.SendCommand<TResult>(
			ICommand<TResult> command,
			CancellationToken cancellationToken
		) => sender.Send(command, cancellationToken);
	}
}
