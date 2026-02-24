using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Employees.Dtos;
using System.Net;

namespace Redarbor.Application.Features.Employees.Queries.GetAll;

public sealed class GetAllEmployeeQueryHandler(
        IEmployeeQueryRepository query
    ) : IRequestHandler<GetAllEmployeeQuery, Result<IEnumerable<EmployeeDto>>>
{
    private readonly IEmployeeQueryRepository _query = query;

    public async Task<Result<IEnumerable<EmployeeDto>>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employees = await _query.GetAllAsync();
        return employees.Any()
            ? Result<IEnumerable<EmployeeDto>>.Success(employees)
            : Result<IEnumerable<EmployeeDto>>.Failure("No se encontraron registros de empleados", (int)HttpStatusCode.NotFound);
    }
}
