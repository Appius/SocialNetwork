#region

using System;
using System.Data.Entity.ModelConfiguration;

#endregion

namespace SocialNetwork.Core.Models.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.RoleName)
                .IsRequired()
                .HasMaxLength(256);

            Property(t => t.Description)
                .HasMaxLength(256);

            // Table & Column Mappings
            ToTable("Roles");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.RoleName).HasColumnName("RoleName");
            Property(t => t.Description).HasColumnName("Description");
        }
    }
}