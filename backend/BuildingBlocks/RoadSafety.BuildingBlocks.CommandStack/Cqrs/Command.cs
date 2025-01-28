using ErrorOr;
using MediatR;

namespace RoadSafety.BuildingBlocks.CommandStack.Cqrs
{
	public interface IBaseCommand;

	public interface ICommand : IBaseCommand, IRequest<ErrorOr<Success>>;

	public interface ICommand<TResult> : IBaseCommand, IRequest<ErrorOr<TResult>>;

	public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, ErrorOr<Success>>
		where TCommand : ICommand;

	public interface ICommandHandler<TCommand, TResult>
		: IRequestHandler<TCommand, ErrorOr<TResult>>
		where TCommand : ICommand<TResult>;
}
