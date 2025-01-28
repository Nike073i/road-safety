using MassTransit;
using RoadSafety.BuildingBlocks.CommandStack.Bus;
using RoadSafety.Users.IntegrationEvents;

namespace RoadSafety.Users.Infrastructure.Bus.UserCreated
{
	internal class UserCreatedKeycloakMessageConsumer(IEventPublisher publisher)
		: IConsumer<UserCreatedKeycloakMessage>
	{
		private readonly IEventPublisher _publisher = publisher;

		public Task Consume(ConsumeContext<UserCreatedKeycloakMessage> context)
		{
			var message = context.Message;
			string partWithId = message.ResourcePath.Split('/')[1];
			var userId = Guid.Parse(partWithId);
			var occuredOn = DateTimeOffset.FromUnixTimeMilliseconds(message.Time);
			return _publisher.PublishAsync(
				new UserCreatedIntegrationEvent
				{
					Id = userId,
					UserId = userId,
					OccuredOn = occuredOn,
				}
			);
		}
	}
}
