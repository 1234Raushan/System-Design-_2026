using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Persistence.Configurations;

public sealed class SubjectConfiguration
    : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.ToTable("Subjects");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SubjectName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.SubjectCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.HasIndex(x => x.SubjectCode)
            .IsUnique();
    }
}