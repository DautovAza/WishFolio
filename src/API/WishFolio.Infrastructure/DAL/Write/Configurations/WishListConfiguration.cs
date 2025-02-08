using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Infrastructure.Dal.Write.Configurations;

public class WishListConfiguration : IEntityTypeConfiguration<WishList>
{
    public void Configure(EntityTypeBuilder<WishList> builder)
    {
        builder.ToTable("Wishlists");

        builder.HasKey(wishlist => wishlist.Id);

        builder.Property(wishlist => wishlist.OwnerId)
               .IsRequired();

        builder.Property(wishlist => wishlist.Name)
            .IsRequired()
            .HasMaxLength(WishlistInvariants.NameMaxLength);

        builder.Property(wishlist => wishlist.Description)
            .HasMaxLength(WishlistInvariants.DescriptionMaxLength);

        builder.Property(wishlist => wishlist.Visibility)
            .IsRequired();

        builder.HasMany(wishlist => wishlist.Items)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);
    }
}
