using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redarbor.Domain.Entities.Types;
using Redarbor.Domain.Enums;

namespace Redarbor.Persistence.Configurations.Types;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder
            .ToTable("Companies", SchemeEnum.type.ToString());

        builder
            .Property(d => d.Name)
            .HasMaxLength(40)
            .IsRequired();

        builder
            .HasData(
                [
                    new Company { Id = 1, Name = "RedArbor"}
                ]
            );
    }
}
