using MediatR;
using Redarbor.Application.Common.Results;

namespace Redarbor.Application.Features.Employees.Command.Delete;

public sealed record DeleteEmployeeCommand(
        Guid EmployeeId
    ) : IRequest<Result<Unit>>;