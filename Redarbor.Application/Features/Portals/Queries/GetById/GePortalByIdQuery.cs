using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Master.Dtos;

namespace Redarbor.Application.Features.Portals.Queries.GetById;

public sealed record GePortalByIdQuery(
        int PortalId
    ) : IRequest<Result<MasterDto>>;