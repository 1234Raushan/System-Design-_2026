using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Persistence.EntityConfigurations;

public sealed class FeePaymentConfiguration
    : IEntityTypeConfiguration<FeePayment>
{
    public void Configure(
        EntityTypeBuilder<FeePayment> builder)
    {
        builder.ToTable("FeePayments");


        builder.HasKey(x => x.Id);



        builder.Property(x => x.PaidAmount)
            .HasPrecision(10, 2)
            .IsRequired();



        builder.Property(x => x.PaymentMode)
            .HasMaxLength(50)
            .IsRequired();



        builder.Property(x => x.TransactionNo)
            .HasMaxLength(100);



        builder.Property(x => x.Remarks)
            .HasMaxLength(500);



        builder.HasOne(x => x.FeeAssignment)
            .WithMany(x => x.FeePayments)
            .HasForeignKey(x => x.FeeAssignmentId)
            .OnDelete(DeleteBehavior.Restrict);



        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);


        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);
    }
}