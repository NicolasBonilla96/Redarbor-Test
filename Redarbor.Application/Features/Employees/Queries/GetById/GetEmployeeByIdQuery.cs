using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Employees.Dtos;

namespace Redarbor.Application.Features.Employees.Queries.GetById;

public sealed record GetEmployeeByIdQuery(
        Guid EmployeeId
    ) : IRequest<Result<EmployeeDto>>;