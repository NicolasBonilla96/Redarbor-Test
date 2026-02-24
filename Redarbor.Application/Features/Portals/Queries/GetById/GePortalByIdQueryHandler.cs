using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Master.Dtos;
using System.Net;

namespace Redarbor.Application.Features.Portals.Queries.GetById;

public sealed class GePortalByIdQueryHandler(
        IPortalQueryRepository query
    ) : IRequestHandler<GePortalByIdQuery, Result<MasterDto>>
{
    private readonly IPortalQueryRepository _query = query;

    public async Task<Result<MasterDto>> Handle(GePortalByIdQuery request, CancellationToken cancellationToken)
    {
        var portal = await _query.FindByIdAsync(request.PortalId);
        return portal is not null
            ? Result<MasterDto>.Success(portal)
            : Result<MasterDto>.Failure($"No se encontró ningún portal bajo el id '{request.PortalId}'", (int)HttpStatusCode.NotFound);
    }
}
