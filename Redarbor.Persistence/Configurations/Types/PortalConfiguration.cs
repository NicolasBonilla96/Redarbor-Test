using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redarbor.Domain.Entities.Types;
using Redarbor.Domain.Enums;

namespace Redarbor.Persistence.Configurations.Types;

public class PortalConfiguration : IEntityTypeConfiguration<Portal>
{
    public void Configure(EntityTypeBuilder<Portal> builder)
    {
        builder
            .ToTable("Portals", SchemeEnum.type.ToString());

        builder
            .Property(d => d.Name)
            .HasMaxLength(40)
            .IsRequired();

        builder
            .HasData(
                [
                    new Portal { Id = 1, Name = "Computrabajo" },
                    new Portal { Id = 2, Name = "Infojob" }
                ]
            );
    }
}
