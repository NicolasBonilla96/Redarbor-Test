using Redarbor.Application.Core.Abstractions.Services;
using System.Security.Claims;

namespace Redarbor.Api.Services;

public class CurrentUser(
        IHttpContextAccessor contextAccessor
    ) : IUser
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    public string? Id => _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}
