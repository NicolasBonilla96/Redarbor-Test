using Ardalis.Specification;
using Redarbor.Domain.Entities.Info;

namespace Redarbor.Domain.Entities.Types;

public class Company : IEntity<int>
{
    #region Properties

    public int Id { get; set; }

    public string Name { get; set; }

    #endregion

    #region Relations

    public virtual ICollection<Employee> Employees { get; set; } = [];

    #endregion

    #region Functions

    public static Company CreateCompany(string name)
        => new Company
        {
            Name = name
        };

    #endregion
}
