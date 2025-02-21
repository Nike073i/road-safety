using ErrorOr;
using RoadSafety.Articles.CommandStack.Articles.UpdateContent;
using RoadSafety.Articles.Contracts.Moderator.Articles.UpdateContent;
using RoadSafety.BuildingBlocks.Api.Endpoints;
using RoadSafety.BuildingBlocks.CommandStack.Cqrs;

namespace RoadSafety.Articles.Api.Endpoints.Moderator.Articles.UpdateContent
{
	public class UpdateContentEndpoint
	{
		public static Task<IResult> Handle(
			[AsParameters] UpdateContentRequest input,
			ICommandDispatcher dispatcher,
			CancellationToken cancellationToken
		) =>
			input
				.ToErrorOr()
				.Then(input => new UpdateContentCommand(
					input.ArticleId,
					input.UpdateSettings?.ScheduledTime,
					input.UpdateSettings?.Version
				))
				.ThenAsync(req => dispatcher.SendCommand(req, cancellationToken))
				.MatchResult(value =>
					TypedResults.Created(string.Empty, new UpdateContentResponse(value))
				);
	}
}
