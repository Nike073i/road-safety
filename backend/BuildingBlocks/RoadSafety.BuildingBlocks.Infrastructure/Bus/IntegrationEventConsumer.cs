using MassTransit;
using MediatR;
using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.BuildingBlocks.Infrastructure.Bus
{
	public abstract class IntegrationEventConsumer<TIntegrationEvent>(IPublisher publisher)
		: IConsumer<TIntegrationEvent>
		where TIntegrationEvent : IntegrationEvent
	{
		private readonly IPublisher _publisher = publisher;

		public Task Consume(ConsumeContext<TIntegrationEvent> context) =>
			_publisher.Publish(context.Message, context.CancellationToken);
	}
}
