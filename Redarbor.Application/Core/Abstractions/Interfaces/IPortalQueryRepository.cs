using Redarbor.Application.Features.Master.Dtos;

namespace Redarbor.Application.Core.Abstractions.Interfaces;

public interface IPortalQueryRepository
{
    Task<MasterDto?> FindByIdAsync(int id);

    Task<IEnumerable<MasterDto>> GetAllAsync();
}
