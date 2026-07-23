using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Persistence.EntityConfigurations;

public sealed class AttendanceSessionConfiguration
    : IEntityTypeConfiguration<AttendanceSession>
{
    public void Configure(EntityTypeBuilder<AttendanceSession> builder)
    {
        builder.ToTable("AttendanceSessions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.AttendanceDate)
            .IsRequired();

        builder.HasOne(x => x.TeachingAssignment)
            .WithMany()
            .HasForeignKey(x => x.TeachingAssignmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // One attendance per TeachingAssignment per Date
        builder.HasIndex(x => new
        {
            x.TeachingAssignmentId,
            x.AttendanceDate
        })
        .IsUnique();

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);
    }
}