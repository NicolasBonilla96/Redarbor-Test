using Redarbor.Application.Features.Roles.Dtos;

namespace Redarbor.Application.Core.Abstractions.Interfaces;

public interface IRoleQueryRepository
{
    Task<RoleDto?> FindByIdAsync(Guid id);

    Task<IEnumerable<RoleDto>> GetAllAsync();
}
