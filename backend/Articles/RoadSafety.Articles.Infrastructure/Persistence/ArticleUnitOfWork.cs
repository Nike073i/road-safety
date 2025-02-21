using System.Data.Common;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RoadSafety.Articles.Infrastructure.EventSourcing;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;

namespace RoadSafety.Articles.Infrastructure.Persistence
{
	public class ArticleUnitOfWork : UnitOfWork
	{
		private readonly ArticleDbContext _dbContext;
		private readonly IEventContext _eventContext;

		private ArticleUnitOfWork(
			ArticleDbContext articleDbContext,
			IEventContext eventContext,
			DbConnection connection
		)
			: base(connection)
		{
			_dbContext = articleDbContext;
			_eventContext = eventContext;
		}

		protected override async Task<ErrorOr<Success>> SaveActions(
			CancellationToken cancellationToken
		)
		{
			_dbContext.Database.SetDbConnection(Connection);
			_dbContext.Database.UseTransaction(Transaction);
			await _dbContext.SaveChangesAsync(cancellationToken);
			await _eventContext.SaveChangesAsync(Transaction, cancellationToken);
			return Result.Success;
		}

		public static async Task<ArticleUnitOfWork> CreateAsync(
			ArticleDbContext dbContext,
			BuildingBlocks.QueryStack.Persistance.IConnectionFactory connectionFactory,
			IEventContext eventContext
		)
		{
			var connection = await connectionFactory.OpenConnectionAsync();
			var unitOfWork = new ArticleUnitOfWork(dbContext, eventContext, connection);
			return unitOfWork;
		}
	}
}
