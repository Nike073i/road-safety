using System.Data.Common;
using Marten;
using Marten.Services;
using Npgsql;

namespace RoadSafety.Articles.Infrastructure.EventSourcing
{
	public class EventContext(IDocumentStore documentStore) : IEventContext
	{
		private readonly List<Action<IDocumentSession>> _actions = new();

		public Task<T?> AggregateEventsAsync<T>(
			Guid id,
			int version,
			DbTransaction? transaction,
			CancellationToken cancellationToken
		)
			where T : class
		{
			var session = CreateSession(transaction);
			return session.Events.AggregateStreamAsync<T>(id, version, token: cancellationToken);
		}

		public void ExecuteAction(Action<IDocumentSession> action) => _actions.Add(action);

		public Task SaveChangesAsync(
			DbTransaction? transaction,
			CancellationToken cancellationToken = default
		)
		{
			var session = CreateSession(transaction);
			_actions.ForEach(act => act(session));
			return session.SaveChangesAsync(cancellationToken);
		}

		private IDocumentSession CreateSession(DbTransaction? transaction) =>
			transaction is NpgsqlTransaction npgsqlTransaction
				? documentStore.LightweightSession(SessionOptions.ForTransaction(npgsqlTransaction))
				: documentStore.LightweightSession();
	}
}
