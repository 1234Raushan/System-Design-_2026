using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Persistence.EntityConfigurations;

public sealed class ReceiptConfiguration
    : IEntityTypeConfiguration<Receipt>
{
    public void Configure(
        EntityTypeBuilder<Receipt> builder)
    {
        builder.ToTable("Receipts");


        builder.HasKey(x => x.Id);



        builder.Property(x => x.ReceiptNumber)
            .HasMaxLength(100)
            .IsRequired();



        builder.Property(x => x.Amount)
            .HasPrecision(10, 2)
            .IsRequired();



        builder.HasOne(x => x.FeePayment)
            .WithOne()
            .HasForeignKey<Receipt>(x => x.FeePaymentId)
            .OnDelete(DeleteBehavior.Restrict);



        builder.HasIndex(x => x.ReceiptNumber)
            .IsUnique();



        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);


        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);
    }
}