using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Auth.Dtos;

namespace Redarbor.Application.Core.Abstractions.Services;

public interface IAuthService
{
    Task<Result<LoginResponse>> Login(LoginRequest request);

    Task<Result<LoginResponse>> Refresh(string accessToken, string refreshToken);
}
