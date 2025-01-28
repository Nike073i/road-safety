using System.Net.Http.Headers;
using RoadSafety.Users.Infrastructure.Authentication.Settings;

namespace RoadSafety.Users.Infrastructure.Authentication.DelegatingHandlers
{
	internal class AdminAccountAuthenticationHttpHandling(
		IAuthenticationService service,
		ServiceAccountSettings accountSettings
	) : DelegatingHandler
	{
		private readonly IAuthenticationService _service = service;
		private readonly ServiceAccountSettings _accountSettings = accountSettings;

		protected override async Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken
		)
		{
			if (request.Headers.Authorization is not null)
				return await base.SendAsync(request, cancellationToken);

			var token = await _service.AuthenticateServiceAccountAsync(
				_accountSettings,
				cancellationToken
			);
			request.Headers.Authorization = new AuthenticationHeaderValue(
				token.TokenType,
				token.AccessToken
			);
			return await base.SendAsync(request, cancellationToken);
		}
	}
}
