using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Infrastructure.Dal.Write.Configurations;

public class WishListItemConfiguration : IEntityTypeConfiguration<WishlistItem>
{
    public void Configure(EntityTypeBuilder<WishlistItem> builder)
    {
        builder.ToTable("WishlistItems");

        builder.HasKey(item => item.Id);

        builder.Property(item => item.Name)
               .IsRequired()
               .HasMaxLength(WishlistItemInvariants.NameMaxLength);

        builder.Property(item => item.Description)
            .HasMaxLength(WishlistItemInvariants.DescriptionMaxLength);

        builder.OwnsOne(item => item.Link, e =>
        {
            e.Property(d => d.Uri)
             .HasColumnName("Uri");
        });
    }
}
