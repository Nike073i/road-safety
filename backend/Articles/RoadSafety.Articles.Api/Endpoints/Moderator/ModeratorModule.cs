using Carter;
using RoadSafety.Articles.Api.Endpoints.Moderator.Articles;
using RoadSafety.BuildingBlocks.Api.Endpoints;

namespace RoadSafety.Articles.Api.Endpoints.Moderator
{
	public class ModeratorModule : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app) =>
			app.AddSubmodule<ArticleModule>("articles");
	}
}
