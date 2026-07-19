using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Persistence.Configurations;

public sealed class TeacherClassConfiguration
    : IEntityTypeConfiguration<TeacherClass>
{
    public void Configure(EntityTypeBuilder<TeacherClass> builder)
    {
        builder.ToTable("TeacherClasses");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new
        {
            x.TeacherId,
            x.ClassId
        }).IsUnique();

        builder.HasOne(x => x.Teacher)
            .WithMany(x => x.TeacherClasses)
            .HasForeignKey(x => x.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Class)
            .WithMany(x => x.TeacherClasses)
            .HasForeignKey(x => x.ClassId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}