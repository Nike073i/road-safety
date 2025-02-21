using System.Data.Common;
using ErrorOr;
using RoadSafety.Articles.Domain.Articles;

namespace RoadSafety.Articles.CommandStack.Persistence
{
	public interface IArticleDao
	{
		Task<ErrorOr<Success>> UpdateContent(
			Article article,
			DbConnection connection,
			DbTransaction? transaction,
			DateTimeOffset currentUtc,
			CancellationToken cancellationToken = default
		);
	}
}
