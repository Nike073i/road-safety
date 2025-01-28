using RoadSafety.BuildingBlocks.Application.Cache;
using RoadSafety.Users.Infrastructure.Authentication.Settings;

namespace RoadSafety.Users.Infrastructure.Authentication.Cache
{
	internal class CacheAuthenticationService(
		ICacheService cacheService,
		IAuthenticationService authenticationService
	) : IAuthenticationService
	{
		private readonly ICacheService _cacheService = cacheService;
		private readonly IAuthenticationService _authenticationService = authenticationService;

		public async Task<AuthorizationToken> AuthenticateServiceAccountAsync(
			ServiceAccountSettings accountSettings,
			CancellationToken cancellationToken
		)
		{
			string cacheKey = _cacheService.GenerateCacheKey(accountSettings);
			var cachedToken = await _cacheService.GetAsync<AuthorizationToken>(
				cacheKey,
				cancellationToken
			);
			if (cachedToken is not null)
				return cachedToken;
			var token = await _authenticationService.AuthenticateServiceAccountAsync(
				accountSettings,
				cancellationToken
			);
			await _cacheService.SetAsync(
				cacheKey,
				token,
				GetTokenExpiration(token),
				cancellationToken
			);
			return token;
		}

		private static TimeSpan GetTokenExpiration(AuthorizationToken authorizationToken) =>
			TimeSpan.FromSeconds(authorizationToken.ExpiresIn) - TimeSpan.FromMinutes(1);
	}
}
