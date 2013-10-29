#region

using System;
using System.Data.Entity.ModelConfiguration;

#endregion

namespace SocialNetwork.Core.Models.Mapping
{
    public class FriendShipMap : EntityTypeConfiguration<FriendShip>
    {
        public FriendShipMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("FriendShips");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Message).HasColumnName("Message");
            Property(t => t.User1Id).HasColumnName("User1Id");
            Property(t => t.User2Id).HasColumnName("User2Id");
            Property(t => t.RequestDate).HasColumnName("RequestDate");

            // Relationships
            HasRequired(t => t.FromUser)
                .WithMany(t => t.RequestedFriends)
                .HasForeignKey(d => d.User1Id);
            HasRequired(t => t.ToUser)
                .WithMany(t => t.ReceivedFriends)
                .HasForeignKey(d => d.User2Id);
        }
    }
}