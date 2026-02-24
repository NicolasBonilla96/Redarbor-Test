using Redarbor.Domain.Entities.Info;
using System.Linq.Expressions;

namespace Redarbor.Domain.Interfaces;

public interface IEmployeeRepository
{
    void Add(Employee employee);

    void Update(Employee employee);

    void Delete(Employee employee);

    Task<bool> AnyAsync(Expression<Func<Employee, bool>> predicate, CancellationToken cancellationToken = default);

    Task<Employee?> FindAsync(Expression<Func<Employee, bool>> predicate, CancellationToken cancellationToken = default);
}
