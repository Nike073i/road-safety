using System.Data.Common;
using Marten;

namespace RoadSafety.Articles.Infrastructure.EventSourcing
{
	public interface IEventContext
	{
		Task SaveChangesAsync(
			DbTransaction? transaction,
			CancellationToken cancellationToken = default
		);
		Task<T?> AggregateEventsAsync<T>(
			Guid id,
			int version,
			DbTransaction? transaction = null,
			CancellationToken cancellationToken = default
		)
			where T : class;
		void ExecuteAction(Action<IDocumentSession> action);
	}
}
