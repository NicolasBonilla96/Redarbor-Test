using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Application.Features.Portals.Dtos;
using Redarbor.Domain.Entities.Types;
using Redarbor.Domain.Interfaces;
using System.Net;

namespace Redarbor.Application.Features.Portals.Commands.Create;

public sealed class CreatePortalCommandHandler(
        IServiceProvider provider
    ) : IRequestHandler<CreatePortalCommand, Result<CreatePortalResponse>>
{
    private readonly IPortalRepository _portalRepo = provider.GetRequiredService<IPortalRepository>();
    private readonly IUnitOfWork _unitOfWork = provider.GetRequiredService<IUnitOfWork>();

    public async Task<Result<CreatePortalResponse>> Handle(CreatePortalCommand request, CancellationToken cancellationToken)
    {
        if (await _portalRepo.AnyAsync(d => d.Name.Equals(request.Name), cancellationToken))
            return Result<CreatePortalResponse>.Failure($"Ya existe un portal bajo el nombre de '{request.Name}'", (int)HttpStatusCode.Conflict);

        var newPortal = Portal.CreatePortal(request.Name);
        _portalRepo.Add(newPortal);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreatePortalResponse>.Success(new CreatePortalResponse { Id = newPortal.Id, Name = newPortal.Name });
    }
}
