﻿
namespace WishFolio.Application.Services.Accounts
{
    public interface IAccountService
    {
        Task<string> LoginAsync(string email, string password);
        Task LogoutAsync(string token);
        Task RegisterAsync(string email, string name, int age, string password);
    }
}