using WishFolio.Domain.Abstractions.Repositories;

namespace WishFolio.Infrastructure.Dal;

public class UnitOfWork : IUnitOfWork
{
    private readonly WishFolioContext _context;

    public UnitOfWork(WishFolioContext context) =>
        _context = context;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
