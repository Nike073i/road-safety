using System.Data.Common;
using ErrorOr;
using RoadSafety.BuildingBlocks.CommandStack.Persistence;

namespace RoadSafety.BuildingBlocks.Infrastructure.Persistence
{
	public abstract class UnitOfWork(DbConnection connection) : IUnitOfWork, IDisposable
	{
		private bool _isDisposed;
		public DbConnection Connection { get; } = connection;
		public DbTransaction? Transaction { get; private set; }

		public async Task<DbTransaction> StartTransactionAsync(CancellationToken cancellationToken)
		{
			CheckIsNotDisposed();
			if (Transaction is not null)
				throw new ApplicationException("Transaction already started");
			Transaction = await Connection.BeginTransactionAsync(cancellationToken);
			return Transaction;
		}

		public async Task<ErrorOr<Success>> CommitAsync(CancellationToken cancellationToken)
		{
			CheckIsNotDisposed();
			var saveResult = await SaveActions(cancellationToken);
			if (saveResult.IsError)
				return saveResult.Errors;

			if (Transaction is not null)
			{
				await Transaction.CommitAsync(cancellationToken);
				Transaction.Dispose();
				Transaction = null;
			}
			return Result.Success;
		}

		public bool IsTransactionStarted()
		{
			CheckIsNotDisposed();
			return Transaction != null;
		}

		protected abstract Task<ErrorOr<Success>> SaveActions(CancellationToken cancellationToken);

		private void CheckIsNotDisposed() => ObjectDisposedException.ThrowIf(_isDisposed, this);

		~UnitOfWork()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool isDisposing)
		{
			if (_isDisposed)
				return;
			if (isDisposing)
			{
				Transaction?.Dispose();
				Transaction = null;
				Connection.Dispose();
			}
			_isDisposed = true;
		}
	}
}
