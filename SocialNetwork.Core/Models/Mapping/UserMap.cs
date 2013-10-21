#region

using System;
using System.Data.Entity.ModelConfiguration;

#endregion

namespace SocialNetwork.Core.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.MiddleName)
                .HasMaxLength(50);

            Property(t => t.Mobile)
                .HasMaxLength(20);

            Property(t => t.Website)
                .HasMaxLength(100);

            Property(t => t.Skype)
                .HasMaxLength(50);

            Property(t => t.CurrentCity)
                .IsFixedLength()
                .HasMaxLength(50);

            Property(t => t.Activies)
                .HasMaxLength(1000);

            Property(t => t.Interests)
                .HasMaxLength(1000);

            Property(t => t.FavoriteMusic)
                .HasMaxLength(1000);

            Property(t => t.FavoriteMovies)
                .HasMaxLength(1000);

            Property(t => t.FavoriteBooks)
                .HasMaxLength(1000);

            Property(t => t.FavoriteGames)
                .HasMaxLength(1000);

            Property(t => t.FavoriteQuotes)
                .HasMaxLength(1000);

            Property(t => t.AboutMe)
                .HasMaxLength(1000);

            // Table & Column Mappings
            ToTable("Users");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.Password).HasColumnName("Password");
            Property(t => t.FirstName).HasColumnName("FirstName");
            Property(t => t.LastName).HasColumnName("LastName");
            Property(t => t.MiddleName).HasColumnName("MiddleName");
            Property(t => t.Birthday).HasColumnName("Birthday");
            Property(t => t.Avatar).HasColumnName("Avatar");
            Property(t => t.Mobile).HasColumnName("Mobile");
            Property(t => t.Sex).HasColumnName("Sex");
            Property(t => t.Website).HasColumnName("Website");
            Property(t => t.Skype).HasColumnName("Skype");
            Property(t => t.CurrentCity).HasColumnName("CurrentCity");
            Property(t => t.Activies).HasColumnName("Activies");
            Property(t => t.Interests).HasColumnName("Interests");
            Property(t => t.FavoriteMusic).HasColumnName("FavoriteMusic");
            Property(t => t.FavoriteMovies).HasColumnName("FavoriteMovies");
            Property(t => t.FavoriteBooks).HasColumnName("FavoriteBooks");
            Property(t => t.FavoriteGames).HasColumnName("FavoriteGames");
            Property(t => t.FavoriteQuotes).HasColumnName("FavoriteQuotes");
            Property(t => t.AboutMe).HasColumnName("AboutMe");
        }
    }
}