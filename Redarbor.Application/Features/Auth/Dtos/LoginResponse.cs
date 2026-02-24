namespace Redarbor.Application.Features.Auth.Dtos;

public class LoginResponse
{
    public Guid UserId { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public DateTime ExpiresAtUtc { get; set; }

    public string[] Roles { get; set; } = [];
}
