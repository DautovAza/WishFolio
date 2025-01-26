using System.Xml.Linq;
using WishFolio.Domain.Entities.WishListAgregate;
using WishFolio.Domain.Entities.WishListAgregate.ValueObjects;

namespace WishFolio.Domain.Errors;

public static partial class DomainErrors
{
    public static class WishListItem
    {
        public static Error NameIsNullOrEmpty() => new Error(nameof(WishlistItem),
            $"Имя предмета виш-листа не может быть пустым или NULL.");

        public static Error NameIsToLong(string name) => new Error(nameof(WishlistItem),
            $"Имя предмета виш-листа ({name}) не может быть длиннее, чем {WishlistItemInvariants.NameMaxLength}.");

        public static Error NameIsToShort(string name) => new Error(nameof(WishlistItem),
            $"Имя предмета виш-листа ({name}) не может быть короче, чем {WishlistItemInvariants.NameMinLength}.");

        public static Error ItemAlreadyExisted(Guid itemId) => new Error(nameof(WishlistItem),
            $"Предмет виш-листа ({itemId}) уже добавлен в виш-лист.");

        public static Error ItemNotFound(Guid itemId) => new Error(nameof(WishlistItem),
            $"Предмет виш-листа ({itemId}) не найден.");

        public static Error WishItemLinkIsNullOrEmpty() => new Error(nameof(WishlistItem),
           $"Ссылка на предмет виш-листа не может быть пустым или NULL.");

        public static Error WishItemLinkInvalidFormat(string link) => new(nameof(WishItemLink),
            $"Ссылка на предмет виш-листа имеет невалидный формат ({link}).");

        public static Error DescriptionIsNull() => new(nameof(WishItemLink),
            $"Описание предмета виш-листа не может быть NULL.");

        public static Error DescriptionIsToLong() => new Error(nameof(WishlistItem),
           $"Описание предмета виш-листа не может быть длиннее, чем {WishlistItemInvariants.DescriptionMaxLength}.");
        
        public static Error ItemAlreadyReserved(string name) => new Error(nameof(WishlistItem),
           $"Предмет виш-листа ({name}) уже зарезервирован.");   
        
        public static Error ItemAlreadyReservedByOtherUser(string name) => new Error(nameof(WishlistItem),
           $"Предмет виш-листа ({name}) зарезервирован другим пользователем.");
    }
}
