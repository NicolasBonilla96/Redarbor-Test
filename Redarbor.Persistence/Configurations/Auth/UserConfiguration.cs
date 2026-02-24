using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redarbor.Domain.Entities.Auth;
using Redarbor.Domain.Enums;

namespace Redarbor.Persistence.Configurations.Auth;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("User", SchemeEnum.auth.ToString());
        
        builder
            .HasMany(d => d.UserRoles)
            .WithOne(d => d.User)
            .HasForeignKey(d => d.UserId)
            .IsRequired();
        
        builder
            .HasMany(d => d.UserLogins)
            .WithOne(d => d.User)
            .HasForeignKey(d => d.UserId)
            .IsRequired();
        
        builder
            .HasMany(d => d.UserTokens)
            .WithOne(d => d.User)
            .HasForeignKey(d => d.UserId)
            .IsRequired();
    }
}
