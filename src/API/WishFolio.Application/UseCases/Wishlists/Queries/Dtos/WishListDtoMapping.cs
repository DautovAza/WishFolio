using AutoMapper;
using WishFolio.Domain.Entities.ReadModels.WishLlists;
using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Application.UseCases.Wishlists.Queries.Dtos;

public class WishListDtoMapping : Profile
{
    public WishListDtoMapping()
    {
        CreateMap<WishlistItemReadModel, WishListItemDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<WishlistItemReadModel, WishListItemDetailsDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Uri, opt => opt.MapFrom(src => src.Uri));

        CreateMap<WishlistReadModel, WishListDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.VisabilityLevel, opt => opt.MapFrom(src => src.Visibility));
    }
}