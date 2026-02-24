using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redarbor.Domain.Entities.Types;
using Redarbor.Domain.Enums;

namespace Redarbor.Persistence.Configurations.Types;

public class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder
            .ToTable("States", SchemeEnum.type.ToString());

        builder
            .Property(d => d.Name)
            .HasMaxLength(40)
            .IsRequired();

        builder
            .HasData(
                [
                    new State { Id = 1, Name = "Activo" },
                    new State { Id = 2, Name = "Inactivo" }
                ]
            );
    }
}
