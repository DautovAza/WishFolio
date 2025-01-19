
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.Accounts
{
    public interface IAccountService
    {
        Task<Result<string>> LoginAsync(string email, string password);
        Task<Result> LogoutAsync(string token);
        Task<Result> RegisterAsync(string email, string name, int age, string password);
    }
}