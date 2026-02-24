using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Employees.Dtos;

namespace Redarbor.Application.Features.Employees.Command.Update;

public sealed record UpdateEmployeeCommand(
        CreateEmployeeRequest Employee
    ) : IRequest<Result<Unit>>;