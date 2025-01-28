using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RoadSafety.Users.Domain.Roles;

namespace RoadSafety.Users.Infrastructure.Persistence.Context
{
	internal class IgnoreEntitiesInterceptor : SaveChangesInterceptor
	{
		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
			DbContextEventData eventData,
			InterceptionResult<int> result,
			CancellationToken cancellationToken = default
		)
		{
			var context = eventData.Context!;
			foreach (var role in context.ChangeTracker.Entries<Role>())
			{
				role.State = EntityState.Unchanged;
			}
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}
	}
}
