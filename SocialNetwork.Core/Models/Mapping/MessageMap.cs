#region

using System;
using System.Data.Entity.ModelConfiguration;

#endregion

namespace SocialNetwork.Core.Models.Mapping
{
    public class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Title)
                .HasMaxLength(50);

            Property(t => t.MsgText)
                .IsRequired();

            // Table & Column Mappings
            ToTable("Messages");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.User1Id).HasColumnName("User1Id");
            Property(t => t.User2Id).HasColumnName("User2Id");
            Property(t => t.Title).HasColumnName("Title");
            Property(t => t.MsgText).HasColumnName("MsgText");
            Property(t => t.PostedDate).HasColumnName("PostedDate");

            // Relationships
            HasRequired(t => t.FromUser)
                .WithMany(t => t.InboxMessages)
                .HasForeignKey(d => d.User1Id);
            HasRequired(t => t.ToUser)
                .WithMany(t => t.SentMessages)
                .HasForeignKey(d => d.User2Id);
        }
    }
}