using System.Text.Json.Serialization;

namespace RoadSafety.Users.Infrastructure.Authentication
{
	internal class AuthorizationToken
	{
		[JsonPropertyName("access_token")]
		public required string AccessToken { get; init; }

		[JsonPropertyName("token_type")]
		public required string TokenType { get; init; }

		[JsonPropertyName("expires_in")]
		public required int ExpiresIn { get; init; }
	}
}
