using WishFolio.Domain.Abstractions.Auth;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace WishFolio.Infrastructure.Auth;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetCurrentUserId()
    {
        var userIdString = _httpContextAccessor.HttpContext?.User?
            .FindFirst(JwtRegisteredClaimNames.NameId)?.Value;

        if (Guid.TryParse(userIdString, out var userId))
        {
            return userId;
        }

        throw new UnauthorizedAccessException("Пользователь не аутентифицирован.");
    }
}
