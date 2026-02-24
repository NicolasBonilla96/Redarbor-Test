using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redarbor.Domain.Entities.Info;
using Redarbor.Domain.Enums;

namespace Redarbor.Persistence.Configurations.Info;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
            .ToTable("Employees", SchemeEnum.info.ToString());

        builder
            .Property(d => d.Name)
            .HasMaxLength(40);

        builder
            .Property(d => d.Fax)
            .HasMaxLength(10);

        builder
            .Property(d => d.IsDeleted)
            .HasDefaultValue(false);

        builder
            .HasOne(d => d.Company)
            .WithMany(d => d.Employees)
            .HasForeignKey(d => d.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(d => d.Portal)
            .WithMany(d => d.Employees)
            .HasForeignKey(d => d.PortalId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(d => d.State)
            .WithMany(d => d.Employees)
            .HasForeignKey(d => d.StateId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(d => d.User)
            .WithOne(d => d.Employee)
            .HasForeignKey<Employee>(d => d.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
