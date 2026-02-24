using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Domain.Entities.Auth;
using Redarbor.Domain.Interfaces;
using System.Net;

namespace Redarbor.Application.Features.Employees.Command.Delete;

public sealed class DeleteEmployeeCommandHandler(
        IServiceProvider provider
    ) : IRequestHandler<DeleteEmployeeCommand, Result<Unit>>
{
    private readonly IEmployeeRepository _employeeRepo = provider.GetRequiredService<IEmployeeRepository>();
    private readonly UserManager<User> _userManager = provider.GetRequiredService<UserManager<User>>();
    private readonly IUnitOfWork _unitOfWork = provider.GetRequiredService<IUnitOfWork>();

    public async Task<Result<Unit>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();

        var employee = await _employeeRepo.FindAsync(d => d.Id.Equals(request.EmployeeId), cancellationToken);
        if (employee is null)
            return Result<Unit>.Failure($"No existe ningún empleado bajo el id '{request.EmployeeId}'", (int)HttpStatusCode.NotFound);

        var user = await _userManager.FindByIdAsync(employee.UserId.ToString());
        
        _employeeRepo.Delete(employee);
        await _userManager.DeleteAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
