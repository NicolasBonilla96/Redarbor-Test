using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.States.Dtos;

namespace Redarbor.Application.Features.States.Commands;

public sealed record CreateStateCommand(
        string Name
    ) : IRequest<Result<CreateStateResponse>>;