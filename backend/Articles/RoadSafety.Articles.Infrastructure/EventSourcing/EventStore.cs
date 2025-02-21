using RoadSafety.Articles.CommandStack.EventSourcing;
using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.Articles.Infrastructure.EventSourcing
{
	public class EventStore(IEventContext eventContext) : IEventStore
	{
		public void AddEvents(Guid aggregateId, params DomainEvent[] domainEvent) =>
			eventContext.ExecuteAction(session => session.Events.Append(aggregateId, domainEvent));

		public Task<TAggregate?> AggregateAsync<TAggregate>(
			Guid aggregateId,
			int version = 0,
			CancellationToken cancellationToken = default
		)
			where TAggregate : class =>
			eventContext.AggregateEventsAsync<TAggregate>(
				aggregateId,
				version,
				cancellationToken: cancellationToken
			);

		public void StartAggregate(Guid aggregateId, DomainEvent domainEvent) =>
			eventContext.ExecuteAction(session =>
				session.Events.StartStream(aggregateId, domainEvent)
			);
	}
}
