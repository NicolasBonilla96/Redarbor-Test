using Redarbor.Domain.Entities.Types;
using Redarbor.Domain.Interfaces;
using Redarbor.Persistence.Contexts;

namespace Redarbor.Persistence.Repositories.EF;

public sealed class PortalRepository(
        RedarborDbContext db
    ) : Repository<Portal, int>(db), IPortalRepository;