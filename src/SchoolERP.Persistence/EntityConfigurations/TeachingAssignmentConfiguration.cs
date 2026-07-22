using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Persistence.Configurations;

public sealed class TeachingAssignmentConfiguration
    : IEntityTypeConfiguration<TeachingAssignment>
{
    public void Configure(EntityTypeBuilder<TeachingAssignment> builder)
    {
        builder.ToTable("TeachingAssignments");

        builder.HasKey(x => x.Id);

        // Relationships

        builder.HasOne(x => x.Teacher)
            .WithMany(x => x.TeachingAssignments)
            .HasForeignKey(x => x.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Subject)
            .WithMany(x => x.TeachingAssignments)
            .HasForeignKey(x => x.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Class)
            .WithMany(x => x.TeachingAssignments)
            .HasForeignKey(x => x.ClassId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Section)
            .WithMany(x => x.TeachingAssignments)
            .HasForeignKey(x => x.SectionId)
            .OnDelete(DeleteBehavior.Restrict);

        // Unique Assignment

        builder.HasIndex(x => new
        {
            x.TeacherId,
            x.SubjectId,
            x.ClassId,
            x.SectionId
        })
        .IsUnique();

        // Indexes
        builder.HasIndex(x => x.TeacherId);
        builder.HasIndex(x => x.SubjectId);
        builder.HasIndex(x => new
        {
            x.ClassId,
            x.SectionId
        });
    }
}