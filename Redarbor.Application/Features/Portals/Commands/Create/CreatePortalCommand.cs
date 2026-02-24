using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Portals.Dtos;

namespace Redarbor.Application.Features.Portals.Commands.Create;

public sealed record CreatePortalCommand(
        string Name
    ) : IRequest<Result<CreatePortalResponse>>;