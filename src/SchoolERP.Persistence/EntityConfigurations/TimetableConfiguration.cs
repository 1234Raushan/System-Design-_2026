using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Persistence.Configurations;

public sealed class TimetableConfiguration
    : IEntityTypeConfiguration<Timetable>
{
    public void Configure(EntityTypeBuilder<Timetable> builder)
    {
        builder.ToTable("Timetables");


        builder.HasKey(x => x.Id);


        builder.Property(x => x.DayOfWeek)
            .IsRequired();


        builder.Property(x => x.PeriodNumber)
            .IsRequired();


        builder.Property(x => x.StartTime)
            .IsRequired();


        builder.Property(x => x.EndTime)
            .IsRequired();


        builder.Property(x => x.RoomNumber)
            .HasMaxLength(50);


        builder.Property(x => x.Remarks)
            .HasMaxLength(500);



        // Relationship
        builder.HasOne(x => x.TeachingAssignment)
            .WithMany(x => x.Timetables)
            .HasForeignKey(x => x.TeachingAssignmentId)
            .OnDelete(DeleteBehavior.Restrict);



        // Same Teaching Assignment cannot have duplicate period on same day
        builder.HasIndex(x => new
        {
            x.TeachingAssignmentId,
            x.DayOfWeek,
            x.PeriodNumber

        })
        .IsUnique();
    }
}