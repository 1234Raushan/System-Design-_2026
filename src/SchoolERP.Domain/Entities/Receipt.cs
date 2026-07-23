using SchoolERP.Domain.Common;

namespace SchoolERP.Domain.Entities;

public sealed class Receipt : BaseAuditableEntity
{
    public int FeePaymentId { get; private set; }


    public string ReceiptNumber { get; private set; }
        = string.Empty;


    public DateOnly ReceiptDate { get; private set; }


    public decimal Amount { get; private set; }



    // Navigation

    public FeePayment FeePayment
    {
        get; private set;
    } = null!;



    private Receipt()
    {
    }



    public Receipt(
        int feePaymentId,
        string receiptNumber,
        DateOnly receiptDate,
        decimal amount)
    {
        FeePaymentId = feePaymentId;

        ReceiptNumber = receiptNumber;

        ReceiptDate = receiptDate;

        Amount = amount;
    }



    public void Update(
        string receiptNumber,
        DateOnly receiptDate)
    {
        ReceiptNumber = receiptNumber;

        ReceiptDate = receiptDate;


        MarkAsUpdated();
    }
}