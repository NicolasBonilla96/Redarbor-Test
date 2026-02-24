using Redarbor.Application.Core.Specifications.Users;
using Redarbor.Domain.Entities.Auth;
using Redarbor.Domain.Interfaces;
using Redarbor.Persistence.Contexts;

namespace Redarbor.Persistence.Repositories.EF;

public sealed class UserRepository(
        RedarborDbContext db
    ) : Repository<User, Guid>(db), IUserRepository
{
    public async Task<User?> FindUserByUserName(string userName)
        => await FindBySpecAsync(new FindByUsernameSpec(userName));
}
