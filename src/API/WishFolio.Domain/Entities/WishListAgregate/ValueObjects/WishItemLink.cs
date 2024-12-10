using WishFolio.Domain.Abstractions;

namespace WishFolio.Domain.Entities.WishListAgregate.ValueObjects
{
    public class WishItemLink : ValueObject
    {
        public string Link { get; }

        public WishItemLink(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                throw new ArgumentException("URL не может быть пустым.");
            }

            if (!Uri.IsWellFormedUriString(link, UriKind.Absolute))
            {
                throw new ArgumentException("Неверный формат URL.");
            }

            Link = link;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Link;
        }
    }
}