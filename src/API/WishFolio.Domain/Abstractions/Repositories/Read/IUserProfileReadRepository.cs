using WishFolio.Domain.Abstractions.ReadModels.Users;

namespace WishFolio.Domain.Abstractions.Repositories.Read;

public interface IUserProfileReadRepository
{
    Task<UserProfileReadModel?> GetByIdAsync(Guid id);
    Task<UserProfileReadModel?> GetByEmailAsync(string email);
}
