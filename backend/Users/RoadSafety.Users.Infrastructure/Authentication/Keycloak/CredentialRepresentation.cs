using System.Text.Json.Serialization;

namespace RoadSafety.Users.Infrastructure.Authentication.Keycloak
{
	internal class CredentialRepresentation
	{
		[JsonPropertyName("temporary")]
		public required bool Temporary { get; set; }

		[JsonPropertyName("type")]
		public required string Type { get; set; }

		[JsonPropertyName("value")]
		public required string Value { get; set; }
	}
}
