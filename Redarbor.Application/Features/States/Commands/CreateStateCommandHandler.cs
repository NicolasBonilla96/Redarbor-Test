using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Application.Features.States.Dtos;
using Redarbor.Domain.Entities.Types;
using Redarbor.Domain.Interfaces;
using System.Net;

namespace Redarbor.Application.Features.States.Commands;

public sealed class CreateStateCommandHandler(
        IServiceProvider provider
    ) : IRequestHandler<CreateStateCommand, Result<CreateStateResponse>>
{
    private readonly IStateRepository _stateRepo = provider.GetRequiredService<IStateRepository>();
    private readonly IUnitOfWork _unitOfWork = provider.GetRequiredService<IUnitOfWork>();

    public async Task<Result<CreateStateResponse>> Handle(CreateStateCommand request, CancellationToken cancellationToken)
    {
        if (await _stateRepo.AnyAsync(d => d.Name.Equals(request.Name), cancellationToken))
            return Result<CreateStateResponse>.Failure($"Ya existe un estado bajo el nombre '{request.Name}'", (int)HttpStatusCode.Conflict);

        var newState = State.CreateState(request.Name);
        _stateRepo.Add(newState);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateStateResponse>.Success(new CreateStateResponse { Id = newState.Id, Name = newState.Name });
    }
}
