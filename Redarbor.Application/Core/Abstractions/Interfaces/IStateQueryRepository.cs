using Redarbor.Application.Features.Master.Dtos;

namespace Redarbor.Application.Core.Abstractions.Interfaces;

public interface IStateQueryRepository
{
    Task<MasterDto?> FindByIdAsync(int id);

    Task<IEnumerable<MasterDto>> GetAllAsync();
}
