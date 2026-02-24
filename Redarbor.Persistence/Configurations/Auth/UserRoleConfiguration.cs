using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redarbor.Domain.Entities.Auth;
using Redarbor.Domain.Enums;

namespace Redarbor.Persistence.Configurations.Auth;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder
            .ToTable("UserRoles", SchemeEnum.auth.ToString());

        builder
            .HasKey(key => new { key.UserId, key.RoleId });
    }
}
