using MediatR;
using RoadSafety.BuildingBlocks.Infrastructure.Bus;
using RoadSafety.Users.IntegrationEvents;

namespace RoadSafety.Users.Infrastructure.Bus.UserCreated
{
	internal class UserCreatedIntegrationEventConsumer(IPublisher publisher)
		: IntegrationEventConsumer<UserCreatedIntegrationEvent>(publisher);
}
