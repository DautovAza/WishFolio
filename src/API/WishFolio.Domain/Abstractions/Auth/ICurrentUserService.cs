using WishFolio.Domain.Entities.UserAgregate;

namespace WishFolio.Domain.Abstractions.Auth;

public interface ICurrentUserService
{
    Task<User> GetCurrentUserAsync();
    Guid GetCurrentUserId();
}
