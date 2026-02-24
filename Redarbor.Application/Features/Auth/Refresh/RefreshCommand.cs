using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Auth.Dtos;

namespace Redarbor.Application.Features.Auth.Refresh;

public sealed record RefreshCommand(
        string AccessToken,
        string RefreshToken
    ) : IRequest<Result<LoginResponse>>;