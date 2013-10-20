using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SocialNetwork.Core.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MiddleName)
                .HasMaxLength(50);

            this.Property(t => t.Mobile)
                .HasMaxLength(20);

            this.Property(t => t.Website)
                .HasMaxLength(100);

            this.Property(t => t.Skype)
                .HasMaxLength(50);

            this.Property(t => t.CurrentCity)
                .IsFixedLength()
                .HasMaxLength(50);

            this.Property(t => t.Activies)
                .HasMaxLength(1000);

            this.Property(t => t.Interests)
                .HasMaxLength(1000);

            this.Property(t => t.FavoriteMusic)
                .HasMaxLength(1000);

            this.Property(t => t.FavoriteMovies)
                .HasMaxLength(1000);

            this.Property(t => t.FavoriteBooks)
                .HasMaxLength(1000);

            this.Property(t => t.FavoriteGames)
                .HasMaxLength(1000);

            this.Property(t => t.FavoriteQuotes)
                .HasMaxLength(1000);

            this.Property(t => t.AboutMe)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.Birthday).HasColumnName("Birthday");
            this.Property(t => t.Avatar).HasColumnName("Avatar");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.Website).HasColumnName("Website");
            this.Property(t => t.Skype).HasColumnName("Skype");
            this.Property(t => t.CurrentCity).HasColumnName("CurrentCity");
            this.Property(t => t.Activies).HasColumnName("Activies");
            this.Property(t => t.Interests).HasColumnName("Interests");
            this.Property(t => t.FavoriteMusic).HasColumnName("FavoriteMusic");
            this.Property(t => t.FavoriteMovies).HasColumnName("FavoriteMovies");
            this.Property(t => t.FavoriteBooks).HasColumnName("FavoriteBooks");
            this.Property(t => t.FavoriteGames).HasColumnName("FavoriteGames");
            this.Property(t => t.FavoriteQuotes).HasColumnName("FavoriteQuotes");
            this.Property(t => t.AboutMe).HasColumnName("AboutMe");
        }
    }
}
