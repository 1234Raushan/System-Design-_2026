using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

public sealed class MarkConfiguration
    : IEntityTypeConfiguration<Mark>
{
    public void Configure(EntityTypeBuilder<Mark> builder)
    {
        builder.ToTable("Marks");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ObtainedMarks)
            .HasPrecision(5, 2);

        builder.Property(x => x.Remarks)
            .HasMaxLength(500);

        builder.HasOne(x => x.Exam)
            .WithMany(x => x.Marks)
            .HasForeignKey(x => x.ExamId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.StudentEnrollment)
            .WithMany(x => x.Marks)
            .HasForeignKey(x => x.StudentEnrollmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.ExamId,
            x.StudentEnrollmentId
        })
        .IsUnique();

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);
    }
}