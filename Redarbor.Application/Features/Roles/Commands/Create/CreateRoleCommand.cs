using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Roles.Dtos;

namespace Redarbor.Application.Features.Roles.Commands.Create;

public sealed record CreateRoleCommand(
        CreateRoleRequest Role
    ) : IRequest<Result<Guid>>;