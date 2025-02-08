using Microsoft.EntityFrameworkCore;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.UserAgregate.Friends;
using WishFolio.Domain.Entities.UserAgregate.Notifications;
using WishFolio.Domain.Entities.WishListAgregate;
using WishFolio.Infrastructure.Dal.Write.Configurations;

namespace WishFolio.Infrastructure.Dal.Write;

public class WishFolioContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Friendship> Friendships { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<WishList> WishLists { get; set; }
    public DbSet<WishlistItem> WishListItems { get; set; }


    public WishFolioContext(DbContextOptions<WishFolioContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new FriendshipConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());
        modelBuilder.ApplyConfiguration(new WishListItemConfiguration());
        modelBuilder.ApplyConfiguration(new WishListConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
