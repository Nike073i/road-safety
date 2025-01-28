using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadSafety.Users.Domain.Roles;

namespace RoadSafety.Users.Infrastructure.Persistence.Configurations
{
	internal class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder) => builder.HasKey(r => r.Code);
	}
}
