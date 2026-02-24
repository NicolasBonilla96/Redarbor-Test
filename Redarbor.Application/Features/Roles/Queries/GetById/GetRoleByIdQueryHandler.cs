using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Roles.Dtos;
using System.Net;

namespace Redarbor.Application.Features.Roles.Queries.GetById;

public sealed class GetRoleByIdQueryHandler(
        IRoleQueryRepository query
    ) : IRequestHandler<GetRoleByIdQuery, Result<RoleDto>>
{
    private readonly IRoleQueryRepository _query = query;

    public async Task<Result<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _query.FindByIdAsync(request.RoleId);
        return role is not null
            ? Result<RoleDto>.Success(role)
            : Result<RoleDto>.Failure($"No se encontró ningún rol bajo el id '{request.RoleId}'", (int)HttpStatusCode.NotFound);
    }
}
