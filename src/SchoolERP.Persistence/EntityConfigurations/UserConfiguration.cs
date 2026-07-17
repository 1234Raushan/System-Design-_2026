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
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table
            builder.ToTable("Users");

            // Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            // First Name
            builder.Property(x => x.FirstName)
                   .HasMaxLength(100)
                   .IsRequired();

            // Last Name
            builder.Property(x => x.LastName)
                   .HasMaxLength(100)
                   .IsRequired();

            // Email
            builder.Property(x => x.Email)
                   .HasMaxLength(256)
                   .IsRequired();

            // Username
            builder.Property(x => x.UserName)
                   .HasMaxLength(100)
                   .IsRequired();

            // Password Hash
            builder.Property(x => x.PasswordHash)
                   .HasMaxLength(500)
                   .IsRequired();

            // Phone Number
            builder.Property(x => x.PhoneNumber)
                   .HasMaxLength(20);

            // Email Confirmation
            builder.Property(x => x.EmailConfirmed)
                   .HasDefaultValue(false);

            // Phone Confirmation
            builder.Property(x => x.PhoneNumberConfirmed)
                   .HasDefaultValue(false);

            // Last Login
            builder.Property(x => x.LastLoginDate);

            // Audit Fields
            builder.Property(x => x.CreatedDate)
                   .IsRequired();

            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);

            builder.Property(x => x.IsDeleted)
                   .HasDefaultValue(false);

            // Unique Indexes
            builder.HasIndex(x => x.Email)
                   .IsUnique();

            builder.HasIndex(x => x.UserName)
                   .IsUnique();

            // Relationships
            builder.HasOne(x => x.Role)
                   .WithMany(x => x.Users)
                   .HasForeignKey(x => x.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
