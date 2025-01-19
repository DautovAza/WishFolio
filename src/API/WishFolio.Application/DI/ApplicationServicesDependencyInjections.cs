using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WishFolio.Application.UseCases.Accounts;
using WishFolio.Application.UseCases.Friends;
using WishFolio.Application.UseCases.Friends.Queries.Dtos;
using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.Application.DI;

public static class ApplicationServicesDependencyInjections
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IValueResolver<Friendship, FriendDto, string>, ProfileNameResolver>();
        services.AddScoped<IAccountService, AccountService>();
        return services;
    }
}