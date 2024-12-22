using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WishFolio.Application.Services.Accounts;
using WishFolio.Application.Services.Friends;
using WishFolio.Application.Services.Friends.Dtos;
using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.Application.DI;

public static class ApplicationServicesDependencyInjections
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IFriendService, FriendService>();
        services.AddScoped<IValueResolver<Friendship, FriendDto, string>, ProfileNameResolver>();
        services.AddScoped<IAccountService, AccountService>();
        return services;
    }
}