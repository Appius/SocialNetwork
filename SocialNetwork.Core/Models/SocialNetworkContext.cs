#region

using System;
using System.Data.Entity;
using SocialNetwork.Core.Models.Mapping;

#endregion

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FriendShipMap());
            modelBuilder.Configurations.Add(new MessageMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}