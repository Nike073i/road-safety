namespace RoadSafety.BuildingBlocks.Api.Auth
{
	public class JwtBearerSettings
	{
		public required string Issuer { get; set; }
		public required string Audience { get; set; }
		public required string MetadataUrl { get; set; }
		public required bool RequireHttpsMetadata { get; set; }
	}
}
