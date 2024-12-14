using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.Infrastructure.Dal.Configurations;

public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
{
    public void Configure(EntityTypeBuilder<Friendship> builder)
    {
        builder.ToTable("Friendships");

        builder.HasKey(f => new { f.UserId, f.FriendId });

        builder.Property(f => f.Status)
               .IsRequired();

        builder.Property(f => f.CreatedAt)
               .IsRequired();
    }
}
