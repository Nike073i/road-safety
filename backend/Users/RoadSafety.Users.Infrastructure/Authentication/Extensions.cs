using Microsoft.Extensions.DependencyInjection;
using RoadSafety.Users.CommandStack.Authentication;
using RoadSafety.Users.Infrastructure.Authentication.Cache;
using RoadSafety.Users.Infrastructure.Authentication.DelegatingHandlers;
using RoadSafety.Users.Infrastructure.Authentication.Keycloak;
using RoadSafety.Users.Infrastructure.Authentication.Settings;

namespace RoadSafety.Users.Infrastructure.Authentication
{
	public static class Extensions
	{
		public static IServiceCollection AddKeycloak(
			this IServiceCollection services,
			KeycloakSettings keycloakSettings,
			ServiceAccountSettings adminServiceAccountSettings
		)
		{
			services.AddTransient<AdminAccountAuthenticationHttpHandling>(sp =>
				new(sp.GetRequiredService<IAuthenticationService>(), adminServiceAccountSettings)
			);
			services.AddHttpClient<IAuthenticationService, AuthenticateService>(c =>
				c.BaseAddress = keycloakSettings.GetOpenIdUri()
			);
			services.Decorate<IAuthenticationService, CacheAuthenticationService>();
			services
				.AddHttpClient<IIdentityProviderClient, KeycloakClient>(c =>
					c.BaseAddress = keycloakSettings.GetAdminUri()
				)
				.AddHttpMessageHandler<AdminAccountAuthenticationHttpHandling>();
			return services;
		}
	}
}
