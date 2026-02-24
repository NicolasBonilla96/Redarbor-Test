using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Services;
using Redarbor.Application.Features.Auth.Dtos;

namespace Redarbor.Application.Features.Auth.Refresh;

public sealed class RefreshCommandHandler(
        IAuthService authService
    ) : IRequestHandler<RefreshCommand, Result<LoginResponse>>
{
    private readonly IAuthService _authService = authService;

    public async Task<Result<LoginResponse>> Handle(RefreshCommand request, CancellationToken cancellationToken)
        => (await _authService.Refresh(request.AccessToken, request.RefreshToken)) is var resp &&
            resp.IsSuccess
                ? Result<LoginResponse>.Success(resp.Value)
                : Result<LoginResponse>.Failure(resp.Error, resp.Code);
}
