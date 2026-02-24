using Redarbor.Domain.Entities.Types;
using System.Linq.Expressions;

namespace Redarbor.Domain.Interfaces;

public interface ICompanyRepository
{
    void Add(Company company);

    Task<bool> AnyAsync(Expression<Func<Company, bool>> predicate, CancellationToken cancellationToken = default);
}
