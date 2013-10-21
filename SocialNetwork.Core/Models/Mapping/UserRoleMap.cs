#region

using System;
using System.Data.Entity.ModelConfiguration;

#endregion

namespace SocialNetwork.Core.Models.Mapping
{
    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("UserRole");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.RoleId).HasColumnName("RoleId");
            Property(t => t.UserId).HasColumnName("UserId");

            // Relationships
            HasRequired(t => t.Role)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(d => d.RoleId);
            HasRequired(t => t.User)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(d => d.UserId);
        }
    }
}