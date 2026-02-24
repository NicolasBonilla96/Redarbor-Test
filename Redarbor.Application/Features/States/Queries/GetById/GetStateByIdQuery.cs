using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Master.Dtos;

namespace Redarbor.Application.Features.States.Queries.GetById;

public sealed record GetStateByIdQuery(
        int StateId
    ) : IRequest<Result<MasterDto>>;