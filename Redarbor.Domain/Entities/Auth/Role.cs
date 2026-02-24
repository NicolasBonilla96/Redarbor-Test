using Ardalis.Specification;
using Microsoft.AspNetCore.Identity;

namespace Redarbor.Domain.Entities.Auth;

public class Role : IdentityRole<Guid>, IEntity<Guid>
{
    #region Properties

    public string Description { get; set; }

    #endregion

    #region Relations

    public virtual ICollection<UserRole> UserRoles { get; set; } = [];

    #endregion

    #region Functions

    public static Role CreateRole(string name, string description)
        => new Role
        {
            Name = name,
            Description = description
        };

    #endregion
}
