using Ardalis.Specification;
using Microsoft.AspNetCore.Identity;
using Redarbor.Domain.Entities.Info;

namespace Redarbor.Domain.Entities.Auth;

public class User : IdentityUser<Guid>, IEntity<Guid>
{
    #region Properties

    public DateTime PasswordExpirationDate { get; set; }

    #endregion

    #region Relations

    public virtual ICollection<UserRole> UserRoles { get; set; } = [];

    public virtual ICollection<UserLogin> UserLogins { get; set; } = [];

    public virtual ICollection<UserToken> UserTokens { get; set; } = [];

    public virtual Employee? Employee { get; set; }

    #endregion

    #region Functions

    public static User CreateUser(string username, string email, string phone)
        => new User
        {
            UserName = username,
            Email = email,
            PhoneNumber = phone,
            PasswordExpirationDate = DateTime.UtcNow.AddDays(30)
        };

    public void SetRefreshToken(string refresh, DateTime expiryDate)
    {
        if (!UserTokens.Any())
            UserTokens.Add(new UserToken(refresh, expiryDate));
        else
        {
            var token = UserTokens.First();
            token.Token = refresh;
            token.ExpiryTime = expiryDate;
        }
    }

    public bool IsValidRefreshToken(string refreshToken)
    {
        var token = UserTokens.FirstOrDefault();
        return token.Token.Equals(refreshToken) && DateTime.UtcNow < token.ExpiryTime;
    }

    #endregion
}
