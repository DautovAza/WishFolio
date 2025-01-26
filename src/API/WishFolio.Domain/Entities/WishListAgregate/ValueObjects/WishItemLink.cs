using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Domain.Entities.WishListAgregate.ValueObjects;

public sealed class WishItemLink : ValueObject
{
    public string Uri { get; private set; }

    private WishItemLink(string uri)
    {
        Uri = uri;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Uri;
    }

    public override string ToString() =>
        Uri;

    public static Result<WishItemLink> Create(string uri)
    {
        if (string.IsNullOrWhiteSpace(uri))
        {
            return Result<WishItemLink>.Failure(DomainErrors.WishListItem.WishItemLinkIsNullOrEmpty());
        }

        if (!System.Uri.IsWellFormedUriString(uri, UriKind.Absolute))
        {
            return Result<WishItemLink>.Failure(DomainErrors.WishListItem.WishItemLinkInvalidFormat(uri));
        }

        var link = new WishItemLink(uri);

        return Result<WishItemLink>.Ok(link);
    }
}