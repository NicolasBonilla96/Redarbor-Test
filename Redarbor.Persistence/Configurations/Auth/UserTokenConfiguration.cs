using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redarbor.Domain.Entities.Auth;
using Redarbor.Domain.Enums;

namespace Redarbor.Persistence.Configurations.Auth;

internal class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder
            .ToTable("UserTokens", SchemeEnum.auth.ToString());

        builder
            .HasKey(key => new { key.UserId, key.LoginProvider, key.Name });
    }
}
