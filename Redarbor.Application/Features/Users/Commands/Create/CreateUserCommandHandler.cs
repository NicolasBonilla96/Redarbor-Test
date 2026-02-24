using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Application.Core.Abstractions.Services;

namespace Redarbor.Application.Features.Users.Commands.Create;

public sealed class CreateUserCommandHandler(
        IServiceProvider provider
    ) : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUserRoleService _authService = provider.GetRequiredService<IUserRoleService>();
    private readonly IUnitOfWork _unitOfWork = provider.GetRequiredService<IUnitOfWork>();

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var resp = await _authService.AddUser(request.User);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return resp.IsSuccess
                ? Result<Guid>.Success(resp.Value.Id)
                : Result<Guid>.Failure(resp.Error, resp.Code);
    }
}
