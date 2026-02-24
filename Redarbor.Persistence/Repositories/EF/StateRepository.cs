using Redarbor.Domain.Entities.Types;
using Redarbor.Domain.Interfaces;
using Redarbor.Persistence.Contexts;

namespace Redarbor.Persistence.Repositories.EF;

public sealed class StateRepository(
        RedarborDbContext db
    ) : Repository<State, int>(db), IStateRepository;