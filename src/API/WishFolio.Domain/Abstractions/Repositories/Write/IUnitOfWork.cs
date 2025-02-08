namespace WishFolio.Domain.Abstractions.Repositories.Write;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}