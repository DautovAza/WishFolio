namespace WishFolio.Domain.Abstractions.Entities;

public class PagedCollection<T>
    where T : class
{
    public PagedCollection(IEnumerable<T> items, int totalItemsCount, int currentPageNumber, int pageSize)
    {
        Items = items;
        TotalItemsCount = totalItemsCount;
        CurrentPageNumber = currentPageNumber;
        PageSize = pageSize;
        TotalPagesCount = (int)Math.Ceiling(totalItemsCount / (double)pageSize);
    }

    public int TotalItemsCount { get; }
    public int CurrentPageNumber { get; }
    public int PageSize { get; }
    public int TotalPagesCount { get; }

    public IEnumerable<T> Items { get; }
}