using System.Data.Entity;

namespace SocialNetwork.Core.Models
{
    public class SocialNetworkContext : DbContext
    {
        static SocialNetworkContext()
        {
            Database.SetInitializer<SocialNetworkContext>(null);
        }

        public SocialNetworkContext()
            : base("Name=SocialNetworkContext")
        {
        }

        public DbSet<FriendShip> FriendShips { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
