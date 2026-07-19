using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Persistence.Configurations;

public sealed class TeacherSubjectConfiguration
    : IEntityTypeConfiguration<TeacherSubject>
{
    public void Configure(EntityTypeBuilder<TeacherSubject> builder)
    {
        builder.ToTable("TeacherSubjects");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new
        {
            x.TeacherId,
            x.SubjectId
        }).IsUnique();

        builder.HasOne(x => x.Teacher)
            .WithMany(x => x.TeacherSubjects)
            .HasForeignKey(x => x.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Subject)
            .WithMany(x => x.TeacherSubjects)
            .HasForeignKey(x => x.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
