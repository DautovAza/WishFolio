using AutoMapper;
using WishFolio.Domain.Abstractions.Entities;

namespace WishFolio.Application.Common.Converters;

public class PageCollectionConverter<TSource, TDestination> : ITypeConverter<PagedCollection<TSource>, PagedCollection<TDestination>>
    where TDestination : class
    where TSource : class
{
    private readonly IMapper _mapper;

    public PageCollectionConverter(IMapper mapper)
    {
        _mapper = mapper;
    }

    public PagedCollection<TDestination> Convert(PagedCollection<TSource> source, PagedCollection<TDestination> destination, ResolutionContext context)
    {
        var items = _mapper.Map<List<TDestination>>(source.Items);

        return new PagedCollection<TDestination>(items, source.TotalItemsCount,source.CurrentPageNumber, source.PageSize);
    }
}
