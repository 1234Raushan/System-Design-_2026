using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Persistence.EntityConfigurations;

public sealed class StudentAttendanceConfiguration
    : IEntityTypeConfiguration<Student_Attendance>
{
    public void Configure(EntityTypeBuilder<Student_Attendance> builder)
    {
        builder.ToTable("StudentAttendances");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Remarks)
            .HasMaxLength(500);

        // Attendance Session
        builder.HasOne(x => x.AttendanceSession)
            .WithMany(x => x.StudentAttendances)
            .HasForeignKey(x => x.AttendanceSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Student Enrollment
        builder.HasOne(x => x.StudentEnrollment)
            .WithMany(x => x.StudentAttendances)
            .HasForeignKey(x => x.StudentEnrollmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // One attendance per enrollment per session
        builder.HasIndex(x => new
        {
            x.AttendanceSessionId,
            x.StudentEnrollmentId
        })
        .IsUnique();

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);
    }
}