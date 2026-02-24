using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Master.Dtos;
using System.Net;

namespace Redarbor.Application.Features.States.Queries.GetById;

public sealed class GetStateByIdQueryHandler(
        IStateQueryRepository query
    ) : IRequestHandler<GetStateByIdQuery, Result<MasterDto>>
{
    private readonly IStateQueryRepository _query = query;

    public async Task<Result<MasterDto>> Handle(GetStateByIdQuery request, CancellationToken cancellationToken)
    {
        var state = await _query.FindByIdAsync(request.StateId);
        return state is not null
            ? Result<MasterDto>.Success(state)
            : Result<MasterDto>.Failure($"No se encontró ningún estado bajo el id '{request.StateId}'.", (int)HttpStatusCode.NotFound);
    }
}
