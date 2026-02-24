using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Roles.Dtos;
using System.Net;

namespace Redarbor.Application.Features.Roles.Queries.GetAll;

public sealed class GetAllRolesQueryHandler(
        IRoleQueryRepository query
    ) : IRequestHandler<GetAllRolesQuery, Result<IEnumerable<RoleDto>>>
{
    private readonly IRoleQueryRepository _query = query;

    public async Task<Result<IEnumerable<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _query.GetAllAsync();
        return roles.Any()
            ? Result<IEnumerable<RoleDto>>.Success(roles)
            : Result<IEnumerable<RoleDto>>.Failure("No se encontró información de roles.", (int)HttpStatusCode.NotFound);
    }
}
