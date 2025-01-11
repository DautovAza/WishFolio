using WishFolio.Domain.Abstractions.Entities;

namespace WishFolio.Domain.Entities.WishListAgregate.ValueObjects
{
    public class WishItemLink : ValueObject
    {
        public string Uri { get; private set; }

        public WishItemLink(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentException("URL не может быть пустым.");
            }

            if (!System.Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            {
                throw new ArgumentException("Неверный формат URL.");
            }

            Uri = uri;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Uri;
        }

        public override string ToString() => 
            Uri;
    }
}