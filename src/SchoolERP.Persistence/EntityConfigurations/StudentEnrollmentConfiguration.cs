using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Persistence.EntityConfigurations;

public sealed class StudentEnrollmentConfiguration
    : IEntityTypeConfiguration<StudentEnrollment>
{
    public void Configure(EntityTypeBuilder<StudentEnrollment> builder)
    {
        builder.ToTable("StudentEnrollments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.RollNumber)
            .IsRequired();

        builder.Property(x => x.AdmissionDate)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Remarks)
            .HasMaxLength(500);

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        // Student
        builder.HasOne(x => x.Student)
            .WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Academic Session
        builder.HasOne(x => x.AcademicSession)
            .WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.AcademicSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        // Class
        builder.HasOne(x => x.Class)
            .WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.ClassId)
            .OnDelete(DeleteBehavior.Restrict);

        // Section
        builder.HasOne(x => x.Section)
            .WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.SectionId)
            .OnDelete(DeleteBehavior.Restrict);

        // One Roll Number per Class + Section + Session
        builder.HasIndex(x => new
        {
            x.AcademicSessionId,
            x.ClassId,
            x.SectionId,
            x.RollNumber
        }).IsUnique();
    }
}