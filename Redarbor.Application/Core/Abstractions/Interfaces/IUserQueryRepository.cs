using Redarbor.Application.Features.Users.Dtos;

namespace Redarbor.Application.Core.Abstractions.Interfaces;

public interface IUserQueryRepository
{
    Task<UserDto?> FindByIdAsync(Guid id);

    Task<IEnumerable<UserDto>> GetAllAsync();
}
