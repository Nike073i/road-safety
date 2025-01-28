using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using RoadSafety.BuildingBlocks.Application.Auth;

namespace RoadSafety.BuildingBlocks.Api.Auth
{
	public static class Extensions
	{
		public static IServiceCollection AddJwtAuthentication(
			this IServiceCollection services,
			JwtBearerSettings jwtBearerSettings
		)
		{
			services
				.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters.ValidIssuer = jwtBearerSettings.Issuer;
					options.TokenValidationParameters.ValidAudiences = [jwtBearerSettings.Audience];
					options.RequireHttpsMetadata = jwtBearerSettings.RequireHttpsMetadata;
					options.MetadataAddress = jwtBearerSettings.MetadataUrl;
				});
			return services;
		}

		public static IServiceCollection AddUserContext(this IServiceCollection services)
		{
			services.AddScoped<IUserContext, UserContext>();
			services.AddHttpContextAccessor();
			return services;
		}
	}
}
