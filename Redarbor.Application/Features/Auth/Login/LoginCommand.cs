using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Auth.Dtos;

namespace Redarbor.Application.Features.Auth.Login;

public sealed record LoginCommand(
        LoginRequest Login
    ) : IRequest<Result<LoginResponse>>;