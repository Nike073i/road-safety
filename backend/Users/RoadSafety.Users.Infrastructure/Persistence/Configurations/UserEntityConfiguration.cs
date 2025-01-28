using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadSafety.Users.Domain.Roles;
using RoadSafety.Users.Domain.Users;

namespace RoadSafety.Users.Infrastructure.Persistence.Configurations
{
	internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(u => u.Id);
			builder
				.HasMany(u => u.Roles)
				.WithMany()
				.UsingEntity<UserRoles>(builder =>
				{
					builder.HasKey(u => new { u.UserId, u.RoleCode });
					builder.HasOne<User>().WithMany().HasForeignKey(ur => ur.UserId);
					builder.HasOne<Role>().WithMany().HasForeignKey(ur => ur.RoleCode);
				});
		}
	}
}
