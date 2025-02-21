using Microsoft.FeatureManagement;
using RoadSafety.Articles.Api.Endpoints;
using RoadSafety.BuildingBlocks.Api.Auth;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace RoadSafety.Articles.Api
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApi(
			this IServiceCollection services,
			JwtBearerSettings jwtBearerSettings
		)
		{
			services.AddFluentValidationAutoValidation();
			services.AddFeatureManagement();
			services.AddJwtAuthentication(jwtBearerSettings);
			services.AddAuthorization();
			services.AddUserContext();
			services.AddEndpoints();
			return services;
		}
	}
}
