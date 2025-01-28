using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace RoadSafety.BuildingBlocks.Api.Endpoints
{
	public static class CarterExtensions
	{
		public static RouteGroupBuilder AddSubmodule<TModule>(
			this IEndpointRouteBuilder parent,
			string prefix
		)
			where TModule : ICarterModule, new()
		{
			var module = new TModule();
			var subgroup = parent.MapGroup(prefix);
			module.AddRoutes(subgroup);
			return subgroup;
		}
	}
}
