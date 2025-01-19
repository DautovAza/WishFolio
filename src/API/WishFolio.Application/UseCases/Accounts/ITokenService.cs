using WishFolio.Domain.Entities.UserAgregate;

namespace WishFolio.Application.UseCases.Accounts;

public interface ITokenService
{
    string GenerateToken(User user);
}