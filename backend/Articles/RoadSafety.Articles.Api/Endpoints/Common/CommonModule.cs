using Carter;
using RoadSafety.Articles.Api.Endpoints.Common.Articles;
using RoadSafety.BuildingBlocks.Api.Endpoints;

namespace RoadSafety.Articles.Api.Endpoints.Common
{
	public class CommonModule : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app) =>
			app.AddSubmodule<ArticleModule>("articles");
	}
}
