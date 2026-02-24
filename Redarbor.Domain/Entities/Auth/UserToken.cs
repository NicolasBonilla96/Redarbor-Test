using Microsoft.AspNetCore.Identity;

namespace Redarbor.Domain.Entities.Auth;

public class UserToken : IdentityUserToken<Guid>
{
    #region Properies

    public string? Token { get; set; }

    public DateTime? ExpiryTime { get; set; }

    public UserToken(string? refreshToken, DateTime? expirationDate)
    {
        LoginProvider = "system";
        Name = "refresh";
        Token = refreshToken;
        ExpiryTime = expirationDate;
    }

    public UserToken()
    {
        
    }

    #endregion

    #region Relations

    public virtual User User { get; set; }

    #endregion
}
