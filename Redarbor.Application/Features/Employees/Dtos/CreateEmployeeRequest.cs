namespace Redarbor.Application.Features.Employees.Dtos;

public sealed record CreateEmployeeRequest(
        Guid? EmployeeId,
        int CompanyId,
        string Email,
        string Fax,
        string Name,
        string Password,
        int PortalId,
        int StateId,
        string Phone,
        string Username,
        Guid RoleId
    );