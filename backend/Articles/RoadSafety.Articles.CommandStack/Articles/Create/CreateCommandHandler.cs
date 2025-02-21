using ErrorOr;
using RoadSafety.Articles.CommandStack.EventSourcing;
using RoadSafety.Articles.Domain.Articles;
using RoadSafety.Articles.Domain.Articles.Blocks;
using RoadSafety.BuildingBlocks.Application.Auth;
using RoadSafety.BuildingBlocks.Application.Clock;
using RoadSafety.BuildingBlocks.CommandStack.Cqrs;
using RoadSafety.BuildingBlocks.CommandStack.Persistence;

namespace RoadSafety.Articles.CommandStack.Articles.Create
{
	public class CreateCommandHandler(
		IDateTimeProvider dateTimeProvider,
		IUnitOfWork unitOfWork,
		IUserContext userContext,
		IEventStore eventStore
	) : ICommandHandler<CreateCommand, Guid>
	{
		private static readonly ArticleBlock[] _defaultBlocks = [];

		public Task<ErrorOr<Guid>> Handle(
			CreateCommand request,
			CancellationToken cancellationToken
		) =>
			ErrorOrFactory
				.From(userContext.UserInfo)
				.FailIf(info => info is null, ArticleErrors.UserUnauthorizedError)
				.Then(userInfo =>
					Article.CreateDefault(
						userInfo!.UserId,
						dateTimeProvider.UtcNow,
						request.Title,
						request.Subtitle,
						request.Image,
						request.Tags,
						_defaultBlocks
					)
				)
				.ThenDo(@event => eventStore.StartAggregate(@event.ArticleId, @event))
				.ThenDoAsync(_ => unitOfWork.CommitAsync(cancellationToken))
				.Then(@event => @event.ArticleId);
	}
}
