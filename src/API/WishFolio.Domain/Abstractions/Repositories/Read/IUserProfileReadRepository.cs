using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Entities.ReadModels.Users;

namespace WishFolio.Domain.Abstractions.Repositories.Read;

public interface IUserProfileReadRepository
{
    Task<UserProfileReadModel?> GetByIdAsync(Guid id);
    Task<UserProfileReadModel?> GetByEmailAsync(string email);
    Task<PagedCollection<UserProfileReadModel>> GetFiltredUsersAsync(string? filteringString, string? orderBy, int pageSize, int pageNumber);
}
