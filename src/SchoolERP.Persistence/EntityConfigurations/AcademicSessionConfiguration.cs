using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Persistence.Configurations;

public sealed class AcademicSessionConfiguration
    : IEntityTypeConfiguration<AcademicSession>
{
    public void Configure(EntityTypeBuilder<AcademicSession> builder)
    {
        builder.ToTable("AcademicSessions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SessionName)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.SessionName)
            .IsUnique();

        builder.Property(x => x.Description)
            .HasMaxLength(250);
    }
}