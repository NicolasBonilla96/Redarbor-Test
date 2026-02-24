using System.ComponentModel.DataAnnotations;

namespace Redarbor.Infrastructure.Services.Auth.Settings;

public sealed class JwtSettings
{
    [Required]
    public string Secret { get; set; }

    [Required]
    public string Issuer { get; set; }

    [Required]
    public string Audience { get; set; }

    [Required, Range(0, 20)]
    public int TokenValidityInMinutes { get; set; }

    [Required, Range(0, 20)]
    public int RefreshTokenValidityInMinutes { get; set; }

    public JwtSettings()
    {
        
    }
}
