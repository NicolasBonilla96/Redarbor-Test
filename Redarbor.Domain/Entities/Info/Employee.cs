using Redarbor.Domain.Abstractions;
using Redarbor.Domain.Entities.Auth;
using Redarbor.Domain.Entities.Types;

namespace Redarbor.Domain.Entities.Info;

public class Employee : AuditableEntity
{
    #region Properties

    public string Name { get; set; }

    public string? Fax { get; set; }

    public int CompanyId { get; set; }

    public int PortalId { get; set; }

    public int StateId { get; set; }

    public Guid? UserId { get; set; }

    #endregion

    #region Relations

    public virtual Company Company { get; set; }
    
    public virtual Portal Portal { get; set; }

    public virtual State State { get; set; }

    public virtual User User { get; set; }

    #endregion
}
