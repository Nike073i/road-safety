using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RoadSafety.BuildingBlocks.Infrastructure.Persistence
{
	public static class EntityFrameworkExtensions
	{
		public static void DisableAutoGeneration(this ModelBuilder modelBuilder) =>
			modelBuilder
				.Model.GetEntityTypes()
				.SelectMany(e => e.GetProperties())
				.Where(p => p.IsPrimaryKey())
				.ToList()
				.ForEach(p => p.ValueGenerated = ValueGenerated.Never);
	}
}
