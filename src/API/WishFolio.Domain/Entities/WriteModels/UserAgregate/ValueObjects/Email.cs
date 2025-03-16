using System.Text.RegularExpressions;
using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Domain.Entities.UserAgregate.ValueObjects
{
    public class Email : ValueObject
    {
        private static readonly Regex _emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string Address { get; }

        private Email(string address)
        {
            Address = address;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Address;
        }

        public static Result<Email> Create(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return Result<Email>.Failure(DomainErrors.User.EmailIsNull());
            }

            if (!_emailRegex.IsMatch(address))
            {
                return Result<Email>.Failure(DomainErrors.User.EmailInvalidFormat(address));
            }
            return Result<Email>.Ok(new Email(address));
        }
    }
}