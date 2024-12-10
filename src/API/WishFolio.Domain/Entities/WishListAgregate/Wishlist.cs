namespace WishFolio.Domain.Entities.WishListAgregate;

public class Wishlist
{
    private readonly List<WishlistItem> _items;

    public Guid WishlistId { get; private set; }
    public Guid OwnerId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public VisibilityLevel Visibility { get; private set; }
    public IReadOnlyList<WishlistItem> Items => _items.AsReadOnly();

    public Wishlist(Guid ownerId, string title, string description, VisibilityLevel visibility)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Название списока желаний не может быть пустым.");
        }

        WishlistId = Guid.NewGuid();
        OwnerId = ownerId;
        Title = title;
        Description = description;
        Visibility = visibility;
        _items = new List<WishlistItem>();
    }

    public void Update(string title, string description, VisibilityLevel visibility)
    {
        if (!string.IsNullOrWhiteSpace(title))
        {
            Title = title;
        }
        if (description != null)
        {
            Description = description;
        }

        Visibility = visibility;
    }

    public void AddItem(WishlistItem item)
    {
        if (_items.Any(i => i.ItemId == item.ItemId))
        {
            throw new InvalidOperationException("Элемент уже добавлен в список желаний.");
        }

        _items.Add(item);
    }

    public void RemoveItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(i => i.ItemId == itemId);
        if (item == null)
        {
            throw new KeyNotFoundException("Элемент не найден в списке желаний.");
        }

        _items.Remove(item);
    }
}
