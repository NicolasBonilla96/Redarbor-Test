using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Services;
using Redarbor.Application.Features.Auth.Dtos;

namespace Redarbor.Application.Features.Auth.Login;

public sealed class LoginCommandHandler(
        IAuthService authService
    ) : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IAuthService _authService = authService;

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        => (await _authService.Login(request.Login)) is var resp &&
            resp.IsSuccess
                ? Result<LoginResponse>.Success(resp.Value)
                : Result<LoginResponse>.Failure(resp.Error, resp.Code);
}
