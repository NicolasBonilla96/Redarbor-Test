using Redarbor.Application.Features.Employees.Dtos;

namespace Redarbor.Application.Core.Abstractions.Interfaces;

public interface IEmployeeQueryRepository
{
    Task<EmployeeDto?> FindByIdAsync(Guid id);

    Task<IEnumerable<EmployeeDto>> GetAllAsync();
}
