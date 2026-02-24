using Redarbor.Domain.Entities.Types;
using System.Linq.Expressions;

namespace Redarbor.Domain.Interfaces;

public interface IPortalRepository
{
    void Add(Portal portal);

    Task<bool> AnyAsync(Expression<Func<Portal, bool>> predicate, CancellationToken cancellationToken = default);
}
