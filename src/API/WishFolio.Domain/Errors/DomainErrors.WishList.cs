using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Domain.Errors;

public static partial class DomainErrors
{
    public static class WishList
    {
        public static Error NameIsNullOrEmpty() => new Error(nameof(WishList),
            $"Имя виш-листа не может быть пустым или NULL."); 
        
        public static Error NotFoundByName(string name) => new Error(nameof(WishList),
            $"Виш-лист с именем {name} не найден или недоступен.");   
        
        public static Error NotFoundById(Guid id) => new Error(nameof(WishList),
            $"Виш-лист с id {id} не найден или недоступен.");
        
        public static Error NameIsToLong(string name) => new Error(nameof(WishList),
            $"Имя виш-листа ({name}) не может быть длиннее, чем {WishlistInvariants.NameMaxLength}");

        public static Error DescriptionIsNull() => new Error(nameof(WishList),
            $"Описание виш-листа не может быть NULL.");

        public static Error DescriptionIsToLong() => new Error(nameof(WishList),
            $"Описание виш-листа не может быть длиннее, чем {WishlistInvariants.DescriptionMaxLength}");

        public static Error WishListWithSameNameAlreadyExisted(string listName) => new Error(nameof(WishList),
            $"Имя  виш-листа ({listName}) уже занято для текущего пользователя.");
    }
}
