using Redarbor.Domain.Entities.Auth;

namespace Redarbor.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> FindUserByUserName(string userName);
}
