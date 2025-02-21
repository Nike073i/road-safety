using Carter;
using RoadSafety.Articles.Api.Endpoints.Common;
using RoadSafety.Articles.Api.Endpoints.Moderator;
using RoadSafety.BuildingBlocks.Api.Endpoints;

namespace RoadSafety.Articles.Api.Endpoints
{
	public class RootModule : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.AddSubmodule<ModeratorModule>("moderator").RequireAuthorization();
			app.AddSubmodule<CommonModule>("");
		}
	}
}
