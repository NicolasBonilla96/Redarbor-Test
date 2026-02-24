using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Application.Core.Abstractions.Services;
using Redarbor.Application.Features.Employees.Dtos;
using Redarbor.Application.Features.Users.Dtos;
using Redarbor.Domain.Entities.Auth;
using Redarbor.Domain.Entities.Info;
using Redarbor.Domain.Interfaces;
using System.Net;

namespace Redarbor.Application.Features.Employees.Command.Create;

public sealed class CreateEmployeeCommandHandler(
        IServiceProvider provider
    ) : IRequestHandler<CreateEmployeeCommand, Result<CreateEmployeeResponse>>
{
    private readonly IEmployeeRepository _employeeRepo = provider.GetRequiredService<IEmployeeRepository>();
    private readonly IUserRoleService _userRoleService = provider.GetRequiredService<IUserRoleService>();
    private readonly IUnitOfWork _unitOfWork = provider.GetRequiredService<IUnitOfWork>();
    private readonly UserManager<User> _userManager = provider.GetRequiredService<UserManager<User>>();
    private readonly RoleManager<Role> _roleManager = provider.GetRequiredService<RoleManager<Role>>();
    private readonly ICompanyRepository _companyRepo = provider.GetRequiredService<ICompanyRepository>();
    private readonly IPortalRepository _portalRepo = provider.GetRequiredService<IPortalRepository>();
    private readonly IStateRepository _stateRepo = provider.GetRequiredService<IStateRepository>();

    public async Task<Result<CreateEmployeeResponse>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();

        var role = await _roleManager.FindByIdAsync(request.Employee.RoleId.ToString());
        if (role is null)
            return Result<CreateEmployeeResponse>.Failure($"No existe ningún rol parametrizado bajo el id '{request.Employee.RoleId}'", (int)HttpStatusCode.NotFound);

        if (!await _companyRepo.AnyAsync(d => d.Id.Equals(request.Employee.CompanyId)))
            return Result<CreateEmployeeResponse>.Failure($"No existe ninguna compañía parametrizada bajo el id '{request.Employee.CompanyId}'", (int)HttpStatusCode.NotFound);

        if(!await _portalRepo.AnyAsync(d => d.Id.Equals(request.Employee.PortalId)))
            return Result<CreateEmployeeResponse>.Failure($"No existe ningún portal parametrizado bajo el id '{request.Employee.PortalId}'", (int)HttpStatusCode.NotFound);

        if (!await _stateRepo.AnyAsync(d => d.Id.Equals(request.Employee.StateId)))
            return Result<CreateEmployeeResponse>.Failure($"No existe ningún estado parametrizado bajo el id '{request.Employee.StateId}'", (int)HttpStatusCode.NotFound);

        var existUser = await _userManager.FindByNameAsync(request.Employee.Username);
        
        if (existUser is not null)
            return Result<CreateEmployeeResponse>.Failure($"El usuario '{request.Employee.Username}' ya existe y tiene asignado un empleado", (int)HttpStatusCode.Conflict);

        var requestUser = new CreateUserRequest
        {
            UserName = request.Employee.Username,
            Password = request.Employee.Password,
            Email = request.Employee.Email,
            PhoneNumber = request.Employee.Phone,
            Role = role.Name!
        };
        var responseUser = await _userRoleService.AddUser(requestUser);

        if(!responseUser.IsSuccess)
            return Result<CreateEmployeeResponse>.Failure(responseUser.Error, responseUser.Code);

        var newEmployee = new Employee
        {
            Name = request.Employee.Name,
            Fax = request.Employee.Fax,
            CompanyId = request.Employee.CompanyId,
            PortalId = request.Employee.PortalId,
            StateId = request.Employee.StateId,
            UserId = responseUser.Value.Id
        };
        _employeeRepo.Add(newEmployee);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return Result<CreateEmployeeResponse>.Success(new CreateEmployeeResponse
        {
            EmployeeId = newEmployee.Id,
            CompanyId = newEmployee.CompanyId,
            PortalId = newEmployee.PortalId,
            StateId = newEmployee.StateId,
            Name = newEmployee.Name,
            UserName = responseUser.Value.UserName,
            Password = responseUser.Value.PasswordHash,
            Email = responseUser.Value.Email,
            Phone = responseUser.Value.PhoneNumber,
            Fax = newEmployee.Fax,
            RoleId = role.Id,
            Role = role.Name,
            CreatedOn = newEmployee.CreatedOn,
            CreatedBy = newEmployee.CreatedBy,
            UpdatedOn = newEmployee.UpdatedOn,
            UpdatedBy = newEmployee.UpdatedBy,
            DeletedOn = newEmployee.DeletedOn,
            DeletedBy = newEmployee.DeletedBy
        });
    }
}
