using MassTransit;
using RoadSafety.BuildingBlocks.CommandStack.Bus;
using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.BuildingBlocks.Infrastructure.Bus
{
	public class EventPublisher(IPublishEndpoint endpoint) : IEventPublisher
	{
		private readonly IPublishEndpoint _bus = endpoint;

		public Task PublishAsync<TEvent>(
			TEvent integrationEvent,
			CancellationToken cancellationToken
		)
			where TEvent : IntegrationEvent =>
			_bus.Publish(integrationEvent, c => c.MessageId = c.Message.Id, cancellationToken);
	}
}
