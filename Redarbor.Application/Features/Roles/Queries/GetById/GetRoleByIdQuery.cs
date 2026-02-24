using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Roles.Dtos;

namespace Redarbor.Application.Features.Roles.Queries.GetById;

public sealed record GetRoleByIdQuery(
        Guid RoleId
    ) : IRequest<Result<RoleDto>>;