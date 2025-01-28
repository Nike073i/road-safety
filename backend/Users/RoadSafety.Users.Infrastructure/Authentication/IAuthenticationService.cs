using RoadSafety.Users.Infrastructure.Authentication.Settings;

namespace RoadSafety.Users.Infrastructure.Authentication
{
	internal interface IAuthenticationService
	{
		Task<AuthorizationToken> AuthenticateServiceAccountAsync(
			ServiceAccountSettings accountSettings,
			CancellationToken cancellationToken = default
		);
	}
}
