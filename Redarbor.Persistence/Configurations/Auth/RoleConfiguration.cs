using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redarbor.Domain.Entities.Auth;
using Redarbor.Domain.Enums;

namespace Redarbor.Persistence.Configurations.Auth;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .ToTable("Roles", SchemeEnum.auth.ToString());

        builder
            .HasMany(d => d.UserRoles)
            .WithOne(d => d.Role)
            .HasForeignKey(d => d.RoleId)
            .IsRequired();
    }
}
