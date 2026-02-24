using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Employees.Dtos;
using System.Net;

namespace Redarbor.Application.Features.Employees.Queries.GetById;

public sealed class GetEmployeeByIdQueryHandler(
        IEmployeeQueryRepository query
    ) : IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeDto>>
{
    private readonly IEmployeeQueryRepository _query = query;

    public async Task<Result<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _query.FindByIdAsync(request.EmployeeId);
        return employee is not null
            ? Result<EmployeeDto>.Success(employee)
            : Result<EmployeeDto>.Failure($"No se encontro información de empleado para el id '{request.EmployeeId}'", (int)HttpStatusCode.NotFound);
    }
}
