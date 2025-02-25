﻿using AutoMapper;
using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.Application.UseCases.Friends.Queries.Dtos;

public class FriendDtoMapping : Profile
{
    public FriendDtoMapping()
    {
        CreateMap<Friendship, FriendDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FriendId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Name, opt => opt.MapFrom<ProfileNameResolver>());
    }
}
