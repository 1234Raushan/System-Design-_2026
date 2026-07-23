using SchoolERP.Domain.Common;

namespace SchoolERP.Domain.Entities;

public sealed class FeePayment : BaseAuditableEntity
{
    public int FeeAssignmentId { get; private set; }


    public decimal PaidAmount { get; private set; }


    public DateOnly PaymentDate { get; private set; }


    public string PaymentMode { get; private set; }
        = string.Empty;


    public string? TransactionNo { get; private set; }


    public string? Remarks { get; private set; }



    // Navigation

    public FeeAssignment FeeAssignment
    {
        get; private set;
    } = null!;



    private FeePayment()
    {
    }



    public FeePayment(
        int feeAssignmentId,
        decimal paidAmount,
        DateOnly paymentDate,
        string paymentMode,
        string? transactionNo,
        string? remarks)
    {
        FeeAssignmentId = feeAssignmentId;

        PaidAmount = paidAmount;

        PaymentDate = paymentDate;

        PaymentMode = paymentMode;

        TransactionNo = transactionNo;

        Remarks = remarks;
    }



    public void Update(
        decimal paidAmount,
        DateOnly paymentDate,
        string paymentMode,
        string? transactionNo,
        string? remarks)
    {
        PaidAmount = paidAmount;

        PaymentDate = paymentDate;

        PaymentMode = paymentMode;

        TransactionNo = transactionNo;

        Remarks = remarks;


        MarkAsUpdated();
    }
}