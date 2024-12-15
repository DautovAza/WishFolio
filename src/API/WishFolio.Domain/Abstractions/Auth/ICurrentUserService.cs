namespace WishFolio.Domain.Abstractions.Auth;

public interface ICurrentUserService
{
    Guid GetCurrentUserId();
}
