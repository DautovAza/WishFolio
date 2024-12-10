using WishFolio.Domain.Abstractions;

namespace WishFolio.Domain.Entities.UserAgregate.ValueObjects
{
    public class Password : ValueObject
    {
        public string Hash { get; }

        public Password(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
            {
                throw new ArgumentException("Хэш пароля не может быть пустым.");
            }

            Hash = hash;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
        }
    }
}