using System.Text.RegularExpressions;
using WishFolio.Domain.Abstractions.Entities;

namespace WishFolio.Domain.Entities.UserAgregate.ValueObjects
{
    public class Email : ValueObject
    {
        private static readonly Regex _emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string Address { get; }

        public Email(string address)
        {
            if (string.IsNullOrEmpty(address) || !_emailRegex.IsMatch(address))
            {
                throw new ArgumentException("Неверный формат email.");
            }
            Address = address;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Address;
        }
    }
}