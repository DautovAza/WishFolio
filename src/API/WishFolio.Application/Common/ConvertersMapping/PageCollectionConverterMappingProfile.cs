using AutoMapper;
using WishFolio.Domain.Abstractions.Entities;

namespace WishFolio.Application.Common.Converters;

public class PageCollectionConverterMappingProfile : Profile
{
    public PageCollectionConverterMappingProfile()
    {
        CreateMap(typeof(PagedCollection<>),typeof(PagedCollection<>))
            .ConvertUsing(typeof(PageCollectionConverter<,>));
    }
}