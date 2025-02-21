using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using RoadSafety.Articles.CommandStack.Articles.Create;
using RoadSafety.Articles.Contracts.Moderator.Articles.Create;
using RoadSafety.BuildingBlocks.Api.Endpoints;
using RoadSafety.BuildingBlocks.CommandStack.Cqrs;

namespace RoadSafety.Articles.Api.Endpoints.Moderator.Articles.Create
{
	public class CreateEndpoint
	{
		public static Task<IResult> Handle(
			[FromBody] CreateRequest input,
			ICommandDispatcher dispatcher,
			CancellationToken cancellationToken
		) =>
			input
				.ToErrorOr()
				.Then(input => new CreateCommand(
					input.Title,
					input.Subtitle,
					input.Image,
					input.Tags
				))
				.ThenAsync(req => dispatcher.SendCommand(req, cancellationToken))
				.MatchResult(value =>
					TypedResults.Created(string.Empty, new CreateResponse(value))
				);
	}
}
