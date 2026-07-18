using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Infrastructure.Persistence.Configurations;

public sealed class ClassConfiguration : IEntityTypeConfiguration<Class_A>
{
    public void Configure(EntityTypeBuilder<Class_A> builder)
    {
        builder.ToTable("Class_A");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ClassName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ClassCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.HasIndex(x => x.ClassCode)
            .IsUnique();

        builder.HasIndex(x => x.ClassName)
            .IsUnique();
    }
}