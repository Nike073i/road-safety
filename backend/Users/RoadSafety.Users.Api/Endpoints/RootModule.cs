using Carter;
using RoadSafety.BuildingBlocks.Api.Endpoints;
using RoadSafety.Users.Api.Endpoints.Common;

namespace RoadSafety.Users.Api.Endpoints
{
	public class RootModule : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app) => app.AddSubmodule<CommonModule>("");
	}
}
