using ErrorOr;
using RoadSafety.Articles.CommandStack.EventSourcing;
using RoadSafety.Articles.CommandStack.Persistence;
using RoadSafety.Articles.Domain.Articles;
using RoadSafety.BuildingBlocks.Application.Clock;
using RoadSafety.BuildingBlocks.CommandStack.LongOperations;
using RoadSafety.BuildingBlocks.CommandStack.Persistence;
using RoadSafety.BuildingBlocks.Domain.Extensions;

namespace RoadSafety.Articles.CommandStack.Articles.UpdateContent
{
	public class UpdateContentOperationHandler(
		IDateTimeProvider dateTimeProvider,
		IEventStore articleEventStore,
		IArticleDao articleDao,
		IUnitOfWork unitOfWork
	) : ILongOperationHandler<UpdateContentOperation>
	{
		public Task<ErrorOr<Success>> Handle(
			UpdateContentOperation request,
			CancellationToken cancellationToken
		) =>
			request
				.ToErrorOr()
				.ThenAsync(req =>
					articleEventStore.AggregateAsync<Article>(
						req.ArticleId,
						req.Version,
						cancellationToken
					)
				)
				.FailIf(article => article is null, ArticleErrors.NotFound)
				.ThenAsync(article =>
					articleDao.UpdateContent(
						article!,
						unitOfWork.Connection,
						unitOfWork.Transaction,
						dateTimeProvider.UtcNow
					)
				);
	}
}
