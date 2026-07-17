using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Persistence.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Table Name
            builder.ToTable("Roles");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Id
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            // Name
            builder.Property(x => x.RoleName)
                   .HasMaxLength(100)
                   .IsRequired();

            // Description
            builder.Property(x => x.Description)
                   .HasMaxLength(500);

            // CreatedDate
            builder.Property(x => x.CreatedDate)
                   .IsRequired();

            // IsActive
            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);

            // IsDeleted
            builder.Property(x => x.IsDeleted)
                   .HasDefaultValue(false);

            // Unique Role Name
            builder.HasIndex(x => x.RoleName)
                   .IsUnique();

            // Relationships
            builder.HasMany(x => x.Users)
                   .WithOne(x => x.Role)
                   .HasForeignKey(x => x.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.RolePermissions)
                   .WithOne(x => x.Role)
                   .HasForeignKey(x => x.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
