using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Application.Core.Abstractions.Services;
using Redarbor.Application.Features.Auth.Dtos;
using Redarbor.Domain.Entities.Auth;
using Redarbor.Domain.Interfaces;
using Redarbor.Infrastructure.Services.Auth.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Redarbor.Infrastructure.Services.Auth;

public class AuthService(
        IServiceProvider provider,
        IOptions<JwtSettings> jwtSettings
    ) : IAuthService
{
    private readonly UserManager<User> _userManager = provider.GetRequiredService<UserManager<User>>();
    private readonly IUserRepository _userRepo = provider.GetRequiredService<IUserRepository>();
    private readonly IUnitOfWork _unitOfWork = provider.GetRequiredService<IUnitOfWork>();
    private readonly TokenValidationParameters _tokenValidationParameters = provider.GetRequiredService<TokenValidationParameters>();
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public async Task<Result<LoginResponse>> Login(LoginRequest request)
    {
        var existUser = await _userRepo.FindUserByUserName(request.UserName);
        if (existUser is null)
            return Result<LoginResponse>.Failure($"Usuario o contraseña incorrectos.", (int)HttpStatusCode.Unauthorized);

        var isValidPass = await _userManager.CheckPasswordAsync(existUser, request.Password);
        if(!await _userManager.CheckPasswordAsync(existUser, request.Password))
            return Result<LoginResponse>.Failure($"Usuario o contraseña incorrectos.", (int)HttpStatusCode.Unauthorized);

        if(await _userManager.IsLockedOutAsync(existUser))
            return Result<LoginResponse>.Failure($"El usuario se encuentra bloqueado.", (int)HttpStatusCode.Forbidden);

        var roles = await _userManager.GetRolesAsync(existUser);
        var token = GenerateAccessToken(existUser, roles.ToArray());
        var refreshToken = GenerateRefreshToken();
        existUser.SetRefreshToken(refreshToken, DateTime.UtcNow.AddMinutes(_jwtSettings.TokenValidityInMinutes));
        await _unitOfWork.SaveChangesAsync();

        return Result<LoginResponse>.Success(
            new LoginResponse
            {
                UserId = existUser.Id,
                UserName = existUser.UserName,
                Email = existUser.Email,
                Roles = roles.ToArray(),
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                ExpiresAtUtc = token.ValidTo
            }
        );
    }

    public async Task<Result<LoginResponse>> Refresh(string accessToken, string refreshToken)
    {
        var principalResult = GetClaimsToken(accessToken);
        if (!principalResult.IsSuccess)
            return Result<LoginResponse>.Failure(principalResult.Error, principalResult.Code);

        var userName = principalResult.Value.Identity!.Name;
        var user = await _userRepo.FindUserByUserName(userName!);
        if(user is null)
            return Result<LoginResponse>.Failure($"No se encontró ningún usuario bajo el nombre de usuario '{userName}'", (int)HttpStatusCode.NotFound);

        if(await _userManager.IsLockedOutAsync(user))
            return Result<LoginResponse>.Failure($"El usuario '{userName}' se encuentra bloqueado.", (int)HttpStatusCode.Forbidden);

        var userToken = user.UserTokens.FirstOrDefault();
        if(userToken is null)
            return Result<LoginResponse>.Failure($"No existe un refresh token.", (int)HttpStatusCode.Unauthorized);

        if(!user.IsValidRefreshToken(refreshToken))
            return Result<LoginResponse>.Failure($"Refresh token invalido o expirado.", (int)HttpStatusCode.Unauthorized);

        var newRefresh = GenerateRefreshToken();
        user.SetRefreshToken(newRefresh, DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenValidityInMinutes));
        await _unitOfWork.SaveChangesAsync();
        var newToken = CreateJwtToken(principalResult.Value.Claims.ToList());
        var roles = await _userManager.GetRolesAsync(user);

        return Result<LoginResponse>.Success(
            new LoginResponse
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.ToArray(),
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newToken),
                RefreshToken = newRefresh,
                ExpiresAtUtc = newToken.ValidTo
            }
        );
    }

    private JwtSecurityToken GenerateAccessToken(User user, string[] roles)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()!),
            new(ClaimTypes.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iss, _jwtSettings.Issuer!),
            new(JwtRegisteredClaimNames.Aud, _jwtSettings.Audience!),
        };
        claims.AddRange(roles.Select(role => new Claim("rol", role)));
        return CreateJwtToken(claims);
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> authClaims)
    {
        var secret = _jwtSettings.Secret;
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        return token;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var _randomNumber = RandomNumberGenerator.Create();
        _randomNumber.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private Result<ClaimsPrincipal> GetClaimsToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            return Result<ClaimsPrincipal>.Failure("Token de acceso invalido", (int)HttpStatusCode.Unauthorized);

        return Result<ClaimsPrincipal>.Success(principal);
    }
}
