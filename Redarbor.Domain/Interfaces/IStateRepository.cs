using Redarbor.Domain.Entities.Types;
using System.Linq.Expressions;

namespace Redarbor.Domain.Interfaces;

public interface IStateRepository
{
    void Add(State state);

    Task<bool> AnyAsync(Expression<Func<State, bool>> predicate, CancellationToken cancellationToken = default);
}
