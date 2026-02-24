using Ardalis.Specification;

namespace Redarbor.Domain.Abstractions;

public abstract class AuditableEntity : IEntity<Guid>
{
    public Guid Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }
}
