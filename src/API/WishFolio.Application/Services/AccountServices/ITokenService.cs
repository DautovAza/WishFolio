using WishFolio.Domain.Entities.UserAgregate;

namespace WishFolio.Application.Services.AccountServices;

public interface ITokenService
{
    string GenerateToken(User user);
}