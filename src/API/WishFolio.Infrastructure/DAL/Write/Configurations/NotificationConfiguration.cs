using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.UserAgregate.Notifications;

namespace WishFolio.Infrastructure.Dal.Write.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");

        builder.HasKey(n => n.NotificationId);

        builder.Property(n => n.Type)
               .IsRequired();

        builder.Property(n => n.Message)
               .IsRequired()
               .HasMaxLength(300);

        builder.Property(n => n.IsRead)
               .IsRequired();

        builder.Property(n => n.CreatedAt)
               .IsRequired();

        builder.HasOne<User>()
               .WithMany(u => u.Notifications)
               .HasForeignKey(n => n.UserId);
    }
}