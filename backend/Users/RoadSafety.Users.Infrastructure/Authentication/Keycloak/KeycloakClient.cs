using System.Net;
using System.Net.Http.Json;
using ErrorOr;
using RoadSafety.Users.CommandStack.Authentication;

namespace RoadSafety.Users.Infrastructure.Authentication.Keycloak
{
	internal class KeycloakClient(HttpClient httpClient) : IIdentityProviderClient
	{
		private readonly HttpClient _httpClient = httpClient;

		public async Task<ErrorOr<Guid>> RegisterClientAsync(
			RegisterClientDto registerClientDto,
			CancellationToken cancellationToken
		)
		{
			var userRepresentation = UserRepresentation.FromDto(registerClientDto);
			try
			{
				var response = await _httpClient.PostAsJsonAsync(
					"users",
					userRepresentation,
					cancellationToken
				);
				response.EnsureSuccessStatusCode();
				return ExtractUserId(response);
			}
			catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
			{
				return AuthenticationErrors.UserAlreadyExistsError;
			}
			catch
			{
				return AuthenticationErrors.SomethingWentWrongError;
			}
		}

		private static Guid ExtractUserId(HttpResponseMessage response)
		{
			const string usersSegmentName = "users/";

			string locationHeader =
				(response.Headers.Location?.PathAndQuery)
				?? throw new InvalidOperationException("Location header can't be null");

			string[] segments = locationHeader.Split(usersSegmentName);
			var userId = Guid.Parse(segments[1]);
			return userId;
		}
	}
}
