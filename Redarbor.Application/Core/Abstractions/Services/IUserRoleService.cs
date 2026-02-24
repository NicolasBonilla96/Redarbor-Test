using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Roles.Dtos;
using Redarbor.Application.Features.Users.Dtos;
using Redarbor.Domain.Entities.Auth;

namespace Redarbor.Application.Core.Abstractions.Services;

public interface IUserRoleService
{
    Task<Result<User>> AddUser(CreateUserRequest user);

    Task<Result<User>> UpdateUser(CreateUserRequest user, User userUpdate);

    Task<Result<Role>> AddRole(CreateRoleRequest role);

    Task<Result<User>> GetUser(Guid userId);
}
