using Microsoft.EntityFrameworkCore;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.UserAgregate.Friends;
using WishFolio.Domain.Entities.UserAgregate.Notifications;
using WishFolio.Infrastructure.Dal.Configurations;

namespace WishFolio.Infrastructure.Dal;

public class WishFolioContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Friendship> Friendships { get; set; }
    public DbSet<Notification> Notifications { get; set; }


    public WishFolioContext(DbContextOptions<WishFolioContext> options)
        : base(options)
    {
         
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new FriendshipConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}