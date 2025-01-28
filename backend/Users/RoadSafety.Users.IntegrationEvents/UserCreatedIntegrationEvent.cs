using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.Users.IntegrationEvents
{
	public class UserCreatedIntegrationEvent() : IntegrationEvent()
	{
		public required Guid UserId { get; init; }
	}
}
