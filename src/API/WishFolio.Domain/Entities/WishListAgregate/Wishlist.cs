namespace WishFolio.Domain.Entities.WishListAgregate;

public class WishList
{
    private readonly List<WishlistItem> _items;

    public Guid Id { get; private set; }
    public Guid OwnerId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public VisabilityLevel Visibility { get; private set; }
    public IReadOnlyList<WishlistItem> Items => _items.AsReadOnly();

    public WishList(Guid ownerId, string name, string description, VisabilityLevel visibility, bool isUniqNameForUser)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Название списока желаний не может быть пустым.");
        }

        if (!isUniqNameForUser)
        {
            throw new Exception("Имя виш-листа уже существует для данного пользователя");
        }

        Id = Guid.NewGuid();
        OwnerId = ownerId;
        Name = name;
        Description = description;
        Visibility = visibility;
        _items = new List<WishlistItem>();
    }

    public void Update(string name, string description, VisabilityLevel visibility)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            Name = name;
        }
        if (description != null)
        {
            Description = description;
        }

        Visibility = visibility;
    }

    public void AddItem(WishlistItem item)
    {
        if (_items.Any(i => i.Id == item.Id))
        {
            throw new InvalidOperationException("Элемент уже добавлен в список желаний.");
        }

        _items.Add(item);
    }

    public void RemoveItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            throw new KeyNotFoundException("Элемент не найден в списке желаний.");
        }

        _items.Remove(item);
    }
}
