using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SocialNetwork.Core.Models.Mapping
{
    public class FriendShipMap : EntityTypeConfiguration<FriendShip>
    {
        public FriendShipMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FriendShips");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.User1Id).HasColumnName("User1Id");
            this.Property(t => t.User2Id).HasColumnName("User2Id");
            this.Property(t => t.RequestDate).HasColumnName("RequestDate");

            // Relationships
            this.HasRequired(t => t.UserWhoRequest)
                .WithMany(t => t.RequestedFriends)
                .HasForeignKey(d => d.User1Id);
            this.HasRequired(t => t.UserWhoReceive)
                .WithMany(t => t.ReceivedFriends)
                .HasForeignKey(d => d.User2Id);

        }
    }
}
