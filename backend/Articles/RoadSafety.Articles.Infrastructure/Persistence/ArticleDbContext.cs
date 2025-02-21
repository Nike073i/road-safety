using Microsoft.EntityFrameworkCore;
using RoadSafety.BuildingBlocks.Infrastructure.LongOperations;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;

namespace RoadSafety.Articles.Infrastructure.Persistence
{
	public class ArticleDbContext(DbContextOptions options) : BaseDbContext(options)
	{
		private const string _schema = "articles";

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.HasDefaultSchema(_schema);
			modelBuilder.AddLongOperations(_schema);
		}
	}
}
