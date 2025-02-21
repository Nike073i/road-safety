using ErrorOr;
using RoadSafety.Articles.CommandStack.EventSourcing;
using RoadSafety.Articles.Domain.Articles;
using RoadSafety.BuildingBlocks.Application.Auth;
using RoadSafety.BuildingBlocks.Application.Clock;
using RoadSafety.BuildingBlocks.CommandStack.Cqrs;
using RoadSafety.BuildingBlocks.CommandStack.LongOperations;
using RoadSafety.BuildingBlocks.CommandStack.Persistence;
using RoadSafety.BuildingBlocks.Domain.Extensions;

namespace RoadSafety.Articles.CommandStack.Articles.UpdateContent
{
	public class UpdateContentCommandHandler(
		IDateTimeProvider dateTimeProvider,
		IUnitOfWork unitOfWork,
		IUserContext userContext,
		IEventStore eventStore,
		ILongOperationScheduler scheduler
	) : ICommandHandler<UpdateContentCommand, Guid>
	{
		public Task<ErrorOr<Guid>> Handle(
			UpdateContentCommand request,
			CancellationToken cancellationToken
		) =>
			ErrorOrFactory
				.From(userContext.UserInfo)
				.FailIf(info => info is null, ArticleErrors.UserUnauthorizedError)
				.ThenDoAsync(_ => unitOfWork.StartTransactionAsync())
				.ThenAsync(_ =>
					eventStore.AggregateAsync<Article>(
						request.ArticleId,
						request.Version ?? default,
						cancellationToken: cancellationToken
					)
				)
				.FailIf(article => article is null, ArticleErrors.NotFound)
				.Then(article =>
					article!.UpdateContent(userContext.UserInfo!.UserId, dateTimeProvider.UtcNow)
				)
				.ThenDo(@event => eventStore.AddEvents(request.ArticleId, @event))
				.Then(@event => new UpdateContentOperation(request.ArticleId, @event.PublicVersion))
				.ThenAsync(operation =>
					scheduler.AddOperationAsync(operation, request.ScheduledTime, cancellationToken)
				)
				.ThenDoAsync(_ => unitOfWork.CommitAsync(cancellationToken))
				.Then(id => id);
	}
}
