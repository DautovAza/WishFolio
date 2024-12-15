using WishFolio.Domain.Entities.UserAgregate;

namespace WishFolio.Application.Services.Accounts;

public interface ITokenService
{
    string GenerateToken(User user);
}