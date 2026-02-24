using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Application.Core.Abstractions.Services;
using Redarbor.Application.Features.Users.Dtos;
using Redarbor.Domain.Entities.Auth;
using Redarbor.Domain.Interfaces;
using System.Net;

namespace Redarbor.Application.Features.Employees.Command.Update;

public sealed class UpdateEmployeeCommandHandler(
        IServiceProvider provider
    ) : IRequestHandler<UpdateEmployeeCommand, Result<Unit>>
{
    private readonly IEmployeeRepository _employeeRepo = provider.GetRequiredService<IEmployeeRepository>();
    private readonly IUserRoleService _userRoleService = provider.GetRequiredService<IUserRoleService>();
    private readonly IUnitOfWork _unitOfWork = provider.GetRequiredService<IUnitOfWork>();
    private readonly UserManager<User> _userManager = provider.GetRequiredService<UserManager<User>>();
    private readonly RoleManager<Role> _roleManager = provider.GetRequiredService<RoleManager<Role>>();
    private readonly ICompanyRepository _companyRepo = provider.GetRequiredService<ICompanyRepository>();
    private readonly IPortalRepository _portalRepo = provider.GetRequiredService<IPortalRepository>();
    private readonly IStateRepository _stateRepo = provider.GetRequiredService<IStateRepository>();

    public async Task<Result<Unit>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();

        var employee = await _employeeRepo.FindAsync(d => d.Id.Equals(request.Employee.EmployeeId), cancellationToken);
        if(employee is null)
            return Result<Unit>.Failure($"No existe ningún empleado bajo el id '{request.Employee.EmployeeId}'", (int)HttpStatusCode.NotFound);

        var existUser = await _userManager.FindByIdAsync(employee.UserId.ToString());
        if (existUser is null)
            return Result<Unit>.Failure($"No existe un usuario bajo el nombre '{request.Employee.Username}'", (int)HttpStatusCode.NotFound);

        var role = await _roleManager.FindByIdAsync(request.Employee.RoleId.ToString());
        if (role is null)
            return Result<Unit>.Failure($"No existe ningún rol parametrizado bajo el id '{request.Employee.RoleId}'", (int)HttpStatusCode.NotFound);

        if (!await _companyRepo.AnyAsync(d => d.Id.Equals(request.Employee.CompanyId)))
            return Result<Unit>.Failure($"No existe ninguna compañía parametrizada bajo el id '{request.Employee.CompanyId}'", (int)HttpStatusCode.NotFound);

        if (!await _portalRepo.AnyAsync(d => d.Id.Equals(request.Employee.PortalId)))
            return Result<Unit>.Failure($"No existe ningún portal parametrizado bajo el id '{request.Employee.PortalId}'", (int)HttpStatusCode.NotFound);

        if (!await _stateRepo.AnyAsync(d => d.Id.Equals(request.Employee.StateId)))
            return Result<Unit>.Failure($"No existe ningún estado parametrizado bajo el id '{request.Employee.StateId}'", (int)HttpStatusCode.NotFound);

        var requestUser = new CreateUserRequest
        {
            UserName = request.Employee.Username,
            Password = request.Employee.Password,
            Email = request.Employee.Email,
            PhoneNumber = request.Employee.Phone,
            Role = role.Name!
        };
        var responseUser = await _userRoleService.UpdateUser(requestUser, existUser);

        if (!responseUser.IsSuccess)
            return Result<Unit>.Failure(responseUser.Error, responseUser.Code);

        employee.Name = request.Employee.Name;
        employee.Fax = request.Employee.Fax;
        employee.CompanyId = request.Employee.CompanyId;
        employee.PortalId = request.Employee.PortalId;
        employee.StateId = request.Employee.StateId;
        _employeeRepo.Update(employee);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
