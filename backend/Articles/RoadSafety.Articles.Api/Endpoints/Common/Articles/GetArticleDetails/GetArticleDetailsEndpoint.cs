using System.Text.Json;
using RoadSafety.Articles.Contracts.Common.Articles.GetArticleDetails;
using RoadSafety.Articles.QueryStack.Articles.GetArticleDetails;
using RoadSafety.BuildingBlocks.QueryStack.Cqrs;

namespace RoadSafety.Articles.Api.Endpoints.Common.Articles.GetArticleDetails
{
	public class GetArticleDetailsEndpoint
	{
		public static async Task<IResult> Handle(
			[AsParameters] GetArticleDetailsRequest input,
			IQueryDispatcher dispatcher,
			CancellationToken cancellationToken
		)
		{
			var query = new GetArticleDetailsQuery(input.Id);
			var result = await dispatcher.SendQuery(query, cancellationToken);
			return result is null
				? TypedResults.NotFound()
				: TypedResults.Ok(MapToResponse(result));
		}

		private static GetArticleDetailsResponse MapToResponse(
			GetArticleDetailsViewModel viewModel
		) =>
			new(
				viewModel.Id,
				viewModel.AuthorId,
				viewModel.CreatedAt,
				viewModel.IsArchived,
				viewModel.IsEnableCommenting,
				viewModel.Visibility,
				viewModel.LastEditedAt,
				viewModel.Title,
				viewModel.Subtitle,
				viewModel.Image,
				viewModel.Tags,
				JsonDocument.Parse(viewModel.BlocksJson)
			);
	}
}
