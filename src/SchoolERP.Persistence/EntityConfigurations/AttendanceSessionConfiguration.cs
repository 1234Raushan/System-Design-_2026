using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Persistence.EntityConfigurations;

public sealed class AttendanceSessionConfiguration
    : IEntityTypeConfiguration<AttendanceSession>
{
    public void Configure(
        EntityTypeBuilder<AttendanceSession> builder)
    {
        builder.ToTable("AttendanceSessions");


        builder.HasKey(x => x.Id);


        builder.Property(x => x.AttendanceDate)
            .IsRequired();


        builder.HasOne(x => x.AcademicSession)
            .WithMany()
            .HasForeignKey(x => x.AcademicSessionId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(x => x.Class)
            .WithMany()
            .HasForeignKey(x => x.ClassId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(x => x.Section)
            .WithMany()
            .HasForeignKey(x => x.SectionId)
            .OnDelete(DeleteBehavior.Restrict);


        // One attendance per class-section-date
        builder.HasIndex(x => new
        {
            x.AcademicSessionId,
            x.ClassId,
            x.SectionId,
            x.AttendanceDate

        })
        .IsUnique();


        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);


        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);
    }
}