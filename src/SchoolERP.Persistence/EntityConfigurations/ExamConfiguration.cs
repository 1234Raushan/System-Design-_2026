using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Persistence.Configurations;

public sealed class ExamConfiguration
    : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> builder)
    {
        builder.ToTable("Exams");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExamName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ExamType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.ExamDate)
            .IsRequired();

        builder.Property(x => x.MaximumMarks)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.PassingMarks)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        // Relationship

        builder.HasOne(x => x.TeachingAssignment)
            .WithMany(x => x.Exams)
            .HasForeignKey(x => x.TeachingAssignmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes

        builder.HasIndex(x => x.TeachingAssignmentId);

        builder.HasIndex(x => x.ExamDate);

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);
    }
}