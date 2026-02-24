using Ardalis.Specification;
using Redarbor.Domain.Entities.Info;

namespace Redarbor.Domain.Entities.Types;

public class Portal : IEntity<int>
{
    #region Properties

    public int Id { get; set; }

    public string Name { get; set; }

    #endregion

    #region Relations

    public virtual ICollection<Employee> Employees { get; set; } = [];

    #endregion

    #region Functions

    public static Portal CreatePortal(string name)
        => new Portal
        {
            Name = name
        };

    #endregion
}
