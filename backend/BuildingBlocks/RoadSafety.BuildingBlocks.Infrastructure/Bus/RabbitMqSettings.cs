namespace RoadSafety.BuildingBlocks.Infrastructure.Bus
{
	public class RabbitMqSettings
	{
		public required string Host { get; set; }
		public required string VHost { get; set; }
		public required string Username { get; set; }
		public required string Password { get; set; }
	}
}
