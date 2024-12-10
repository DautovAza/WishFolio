using System.Text.RegularExpressions;
using WishFolio.Domain.Abstractions;

namespace WishFolio.Domain.Entities.UserAgregate.ValueObjects
{
    public class Email : ValueObject
    {
        private static readonly Regex _emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string Address { get; }

        public Email(string email)
        {
            if (string.IsNullOrEmpty(email) || !_emailRegex.IsMatch(email))
            {
                throw new ArgumentException("Неверный формат email.");
            }
            Address = email;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Address;
        }
    }
}