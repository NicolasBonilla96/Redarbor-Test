namespace Redarbor.Application.Features.Auth.Dtos;

public sealed record RefreshRequest(
        string AccessToken,
        string RefreshToken
    );