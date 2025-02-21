using ErrorOr;
using MediatR;

namespace RoadSafety.BuildingBlocks.CommandStack.LongOperations
{
	public interface ILongOperation : IRequest<ErrorOr<Success>>;

	public interface ILongOperationHandler<TOperation>
		: IRequestHandler<TOperation, ErrorOr<Success>>
		where TOperation : ILongOperation;
}
