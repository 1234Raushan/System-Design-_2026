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
    public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            // Table Name
            builder.ToTable("Permissions");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            // Code
            builder.Property(x => x.Code)
                   .HasMaxLength(100)
                   .IsRequired();

            // Name
            builder.Property(x => x.Name)
                   .HasMaxLength(150)
                   .IsRequired();

            // Module
            builder.Property(x => x.Module)
                   .HasMaxLength(100)
                   .IsRequired();

            // Description
            builder.Property(x => x.Description)
                   .HasMaxLength(500);

            // Audit Fields
            builder.Property(x => x.CreatedDate)
                   .IsRequired();

            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);

            builder.Property(x => x.IsDeleted)
                   .HasDefaultValue(false);

            // Unique Index
            builder.HasIndex(x => x.Code)
                   .IsUnique();

            // Relationship
            builder.HasMany(x => x.RolePermissions)
                   .WithOne(x => x.Permission)
                   .HasForeignKey(x => x.PermissionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
