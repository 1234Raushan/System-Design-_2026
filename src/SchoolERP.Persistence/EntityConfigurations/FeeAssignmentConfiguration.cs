using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Persistence.EntityConfigurations;

public sealed class FeeAssignmentConfiguration
    : IEntityTypeConfiguration<FeeAssignment>
{
    public void Configure(
        EntityTypeBuilder<FeeAssignment> builder)
    {
        builder.ToTable("FeeAssignments");


        builder.HasKey(x => x.Id);



        builder.Property(x => x.TotalAmount)
            .HasPrecision(10, 2)
            .IsRequired();


        builder.Property(x => x.PaidAmount)
            .HasPrecision(10, 2)
            .IsRequired();


        builder.Property(x => x.DueAmount)
            .HasPrecision(10, 2)
            .IsRequired();



        builder.HasOne(x => x.StudentEnrollment)
            .WithMany()
            .HasForeignKey(x => x.StudentEnrollmentId)
            .OnDelete(DeleteBehavior.Restrict);



        builder.HasOne(x => x.AcademicSession)
            .WithMany()
            .HasForeignKey(x => x.AcademicSessionId)
            .OnDelete(DeleteBehavior.Restrict);



        // One fee assignment per student per session

        builder.HasIndex(x => new
        {
            x.StudentEnrollmentId,
            x.AcademicSessionId

        })
        .IsUnique();



        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);


        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);
    }
}