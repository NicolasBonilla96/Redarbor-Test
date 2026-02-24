using Ardalis.Specification;
using Redarbor.Domain.Entities.Auth;

namespace Redarbor.Application.Core.Specifications.Users;

public class FindByUsernameSpec : Specification<User>
{
    public FindByUsernameSpec(string userName)
        => Query.Include(d => d.UserTokens)
                .Where(d => d.UserName.Equals(userName));
}
