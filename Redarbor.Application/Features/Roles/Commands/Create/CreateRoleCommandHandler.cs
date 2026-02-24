using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Services;

namespace Redarbor.Application.Features.Roles.Commands.Create;

public sealed class CreateRoleCommandHandler(
        IUserRoleService authService
    ) : IRequestHandler<CreateRoleCommand, Result<Guid>>
{
    private readonly IUserRoleService _authService = authService;

    public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        => (await _authService.AddRole(request.Role)) is var resp &&
            resp.IsSuccess
                ? Result<Guid>.Success(resp.Value.Id)
                : Result<Guid>.Failure(resp.Error, resp.Code);
}
