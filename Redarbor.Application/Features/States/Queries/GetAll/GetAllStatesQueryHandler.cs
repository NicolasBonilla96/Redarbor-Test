using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Master.Dtos;
using System.Net;

namespace Redarbor.Application.Features.States.Queries.GetAll;

public sealed class GetAllStatesQueryHandler(
        IStateQueryRepository query
    ) : IRequestHandler<GetAllStatesQuery, Result<IEnumerable<MasterDto>>>
{
    private readonly IStateQueryRepository _query = query;

    public async Task<Result<IEnumerable<MasterDto>>> Handle(GetAllStatesQuery request, CancellationToken cancellationToken)
    {
        var states = await _query.GetAllAsync();
        return states.Any()
            ? Result<IEnumerable<MasterDto>>.Success(states)
            : Result<IEnumerable<MasterDto>>.Failure("No se encontró información sobre los estados.", (int)HttpStatusCode.NotFound);
    }
}
