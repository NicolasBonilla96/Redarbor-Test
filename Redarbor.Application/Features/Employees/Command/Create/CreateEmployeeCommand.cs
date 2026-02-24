using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Employees.Dtos;

namespace Redarbor.Application.Features.Employees.Command.Create;

public sealed record CreateEmployeeCommand(
        CreateEmployeeRequest Employee
    ) : IRequest<Result<CreateEmployeeResponse>>;