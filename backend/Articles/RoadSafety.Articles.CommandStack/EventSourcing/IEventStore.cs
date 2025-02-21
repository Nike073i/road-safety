using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.Articles.CommandStack.EventSourcing
{
	public interface IEventStore
	{
		void StartAggregate(Guid aggregateId, DomainEvent domainEvent);

		void AddEvents(Guid aggregateId, params DomainEvent[] domainEvent);

		Task<TAggregate?> AggregateAsync<TAggregate>(
			Guid aggregateId,
			int version = 0,
			CancellationToken cancellationToken = default
		)
			where TAggregate : class;
	}
}
