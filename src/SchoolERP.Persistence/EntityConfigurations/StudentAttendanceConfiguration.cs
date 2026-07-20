using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Persistence.EntityConfigurations;

public sealed class StudentAttendanceConfiguration
    : IEntityTypeConfiguration<StudentAttendance>
{
    public void Configure(
        EntityTypeBuilder<StudentAttendance> builder)
    {

        builder.ToTable("StudentAttendances");


        builder.HasKey(x => x.Id);


        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();


        builder.Property(x => x.Remarks)
            .HasMaxLength(500);



        builder.HasOne(x => x.AttendanceSession)
            .WithMany(x => x.StudentAttendances)
            .HasForeignKey(x => x.AttendanceSessionId)
            .OnDelete(DeleteBehavior.Cascade);



        builder.HasOne(x => x.Student)
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict);



        // Duplicate attendance avoid

        builder.HasIndex(x => new
        {
            x.AttendanceSessionId,
            x.StudentId

        })
        .IsUnique();



        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);


        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

    }
}