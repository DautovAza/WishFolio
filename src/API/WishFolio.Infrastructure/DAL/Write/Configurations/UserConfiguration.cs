using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishFolio.Domain.Entities.UserAgregate;

namespace WishFolio.Infrastructure.Dal.Write.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.OwnsOne(u => u.Email, e =>
        {
            e.Property(p => p.Address)
             .HasColumnName("Email")
             .IsRequired();
        });

        builder.Property(u => u.PasswordHash)
               .IsRequired()
               .HasMaxLength(200);

        builder.OwnsOne(u => u.Profile, p =>
        {
            p.Property(pp => pp.Name)
             .HasColumnName("Name")
             .IsRequired();

            p.Property(pp => pp.Age)
             .HasColumnName("Age")
             .IsRequired();
        });

        builder.HasMany(u => u.Friends)
               .WithOne()
               .HasForeignKey(f => f.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Notifications)
               .WithOne()
               .HasForeignKey(n => n.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
