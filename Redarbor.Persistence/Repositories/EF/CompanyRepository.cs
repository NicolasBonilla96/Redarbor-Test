using Redarbor.Domain.Entities.Types;
using Redarbor.Domain.Interfaces;
using Redarbor.Persistence.Contexts;

namespace Redarbor.Persistence.Repositories.EF;

public sealed class CompanyRepository(
        RedarborDbContext db
    ) : Repository<Company, int>(db), ICompanyRepository;