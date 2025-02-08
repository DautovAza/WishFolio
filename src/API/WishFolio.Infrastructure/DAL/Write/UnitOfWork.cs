using WishFolio.Domain.Abstractions.Repositories.Write;

namespace WishFolio.Infrastructure.Dal.Write;

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
