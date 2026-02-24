using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Master.Dtos;
using System.Net;

namespace Redarbor.Application.Features.Portals.Queries.GetAll;

public sealed class GetAllPortalsQueryHandler(
        IPortalQueryRepository query
    ) : IRequestHandler<GetAllPortalsQuery, Result<IEnumerable<MasterDto>>>
{
    private readonly IPortalQueryRepository _query = query;

    public async Task<Result<IEnumerable<MasterDto>>> Handle(GetAllPortalsQuery request, CancellationToken cancellationToken)
    {
        var portals = await _query.GetAllAsync();
        return portals.Any()
            ? Result<IEnumerable<MasterDto>>.Success(portals)
            : Result<IEnumerable<MasterDto>>.Failure("No se encontró información de portales.", (int)HttpStatusCode.NotFound);
    }
}
