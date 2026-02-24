using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Employees.Dtos;

namespace Redarbor.Application.Features.Employees.Queries.GetAll;

public sealed record GetAllEmployeeQuery : IRequest<Result<IEnumerable<EmployeeDto>>>;