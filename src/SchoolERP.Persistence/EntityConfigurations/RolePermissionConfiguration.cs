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
    public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            // Table
            builder.ToTable("RolePermissions");

            // Primary Key
            builder.HasKey(x => new { x.RoleId, x.PermissionId });

            // Foreign Keys
            builder.Property(x => x.RoleId)
                   .IsRequired();

            builder.Property(x => x.PermissionId)
                   .IsRequired();

            // Relationships
            builder.HasOne(x => x.Role)
                   .WithMany(x => x.RolePermissions)
                   .HasForeignKey(x => x.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Permission)
                   .WithMany(x => x.RolePermissions)
                   .HasForeignKey(x => x.PermissionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Prevent Duplicate Mapping
            builder.HasIndex(x => new
            {
                x.RoleId,
                x.PermissionId
            })
            .IsUnique();
        }
    }
}
