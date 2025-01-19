using AutoMapper;
using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Application.UseCases.Wishlists.Dtos;

public class WishListDtoMapping : Profile
{
    public WishListDtoMapping()
    {
        CreateMap<WishlistItem, WishListItemDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        
        CreateMap<WishlistItem, WishListItemDetailsDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Uri, opt => opt.MapFrom(src => src.Link));

        CreateMap<WishList, WishListDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.VisabilityLevel, opt => opt.MapFrom(src => src.Visibility))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            
    }
}