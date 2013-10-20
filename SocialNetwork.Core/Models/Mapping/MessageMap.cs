using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SocialNetwork.Core.Models.Mapping
{
    public class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.MsgText)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Messages");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.User1Id).HasColumnName("User1Id");
            this.Property(t => t.User2Id).HasColumnName("User2Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.MsgText).HasColumnName("MsgText");
            this.Property(t => t.PostedDate).HasColumnName("PostedDate");

            // Relationships
            this.HasRequired(t => t.UserWhoSent)
                .WithMany(t => t.InboxMessages)
                .HasForeignKey(d => d.User1Id);
            this.HasRequired(t => t.UserWhoReceive)
                .WithMany(t => t.SentMessages)
                .HasForeignKey(d => d.User2Id);

        }
    }
}
