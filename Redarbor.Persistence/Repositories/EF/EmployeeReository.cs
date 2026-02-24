using Redarbor.Domain.Entities.Info;
using Redarbor.Domain.Interfaces;
using Redarbor.Persistence.Contexts;

namespace Redarbor.Persistence.Repositories.EF;

public sealed class EmployeeReository(
        RedarborDbContext db
    ) : Repository<Employee, Guid>(db), IEmployeeRepository;