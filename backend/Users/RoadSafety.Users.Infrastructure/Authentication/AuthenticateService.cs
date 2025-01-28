using System.Net;
using System.Net.Http.Json;
using RoadSafety.Users.Infrastructure.Authentication.Exceptions;
using RoadSafety.Users.Infrastructure.Authentication.Settings;

namespace RoadSafety.Users.Infrastructure.Authentication
{
	internal class AuthenticateService(HttpClient httpClient) : IAuthenticationService
	{
		private readonly HttpClient _httpClient = httpClient;

		public async Task<AuthorizationToken> AuthenticateServiceAccountAsync(
			ServiceAccountSettings accountSettings,
			CancellationToken cancellationToken
		)
		{
			var requestParams = new KeyValuePair<string, string>[]
			{
				new("client_id", accountSettings.ClientId),
				new("client_secret", accountSettings.ClientSecret),
				new("scope", "openid email"),
				new("grant_type", "client_credentials"),
			};
			using var formContent = new FormUrlEncodedContent(requestParams);
			var response = await _httpClient.PostAsync("token", formContent, cancellationToken);
			if (response.StatusCode == HttpStatusCode.Unauthorized)
				throw new ServiceAccountAuthenticationException();

			response.EnsureSuccessStatusCode();
			var token = await response.Content.ReadFromJsonAsync<AuthorizationToken>(
				cancellationToken
			);
			return token!;
		}
	}
}
