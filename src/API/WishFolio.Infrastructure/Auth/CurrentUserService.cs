using WishFolio.Domain.Abstractions.Auth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Abstractions.Repositories.Write;

namespace WishFolio.Infrastructure.Auth;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    public Guid GetCurrentUserId()
    {
        var userIdString = _httpContextAccessor.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (Guid.TryParse(userIdString, out var userId))
        {
            return userId;
        }

        throw new UnauthorizedAccessException("Пользователь не аутентифицирован.");
    }

    public async Task<User> GetCurrentUserAsync()
    {
        var id = GetCurrentUserId();

        return await _userRepository.GetByIdAsync(id);
    }
}
