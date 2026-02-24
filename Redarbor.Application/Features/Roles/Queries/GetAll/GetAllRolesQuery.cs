using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Roles.Dtos;

namespace Redarbor.Application.Features.Roles.Queries.GetAll;

public sealed record GetAllRolesQuery : IRequest<Result<IEnumerable<RoleDto>>>;