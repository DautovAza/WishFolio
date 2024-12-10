using System.ComponentModel.DataAnnotations;
using WishFolio.Domain.Entities.WishListAgregate.ValueObjects;

namespace WishFolio.Domain.Entities.WishListAgregate;

public class WishlistItem
{
    [Required]
    public Guid ItemId { get; private set; }

    [Required]
    [MinLength(WishlistItemInvariants.NameMinLength)]
    [MaxLength(WishlistItemInvariants.NameMaxLength)]
    public string Name { get; private set; }

    [MaxLength(WishlistItemInvariants.DescriptionMaxLength)]
    public string Description { get; private set; }

    public WishItemLink Link { get; private set; }

    public WishlistItem(string name, string description, WishItemLink link)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Название элемента не может быть пустым.");
        }

        ItemId = Guid.NewGuid();
        Name = name;
        Description = description;
        Link = link;
    }

    public void Update(string name, string description, WishItemLink link)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            Name = name;
        }
        if (description != null)
        {
            Description = description;
        }
        if (Link != null)
        {
            Link = link;
        }
    }
}