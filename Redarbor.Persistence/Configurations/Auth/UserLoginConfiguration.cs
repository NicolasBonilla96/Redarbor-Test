using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redarbor.Domain.Entities.Auth;
using Redarbor.Domain.Enums;

namespace Redarbor.Persistence.Configurations.Auth;

public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder
            .ToTable("UserLogins", SchemeEnum.auth.ToString());

        builder
            .HasKey(key => new { key.ProviderKey, key.LoginProvider });
    }
}
