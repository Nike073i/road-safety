using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace RoadSafety.BuildingBlocks.Infrastructure.Persistence
{
	public abstract class BaseDbContext(DbContextOptions options) : DbContext(options)
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
			optionsBuilder.UseSnakeCaseNamingConvention();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.DisableAutoGeneration();
			modelBuilder.AddTransactionalOutboxEntities();
		}
	}
}
