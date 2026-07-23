using MediatR;

namespace SchoolERP.Application.Features.Fees.Commands.UpdateFeePayment;

public sealed record UpdateFeePaymentCommand
    : IRequest
{
    public int FeePaymentId { get; init; }


    public decimal PaidAmount { get; init; }


    public DateOnly PaymentDate { get; init; }


    public string PaymentMode { get; init; }
        = string.Empty;


    public string? TransactionNo { get; init; }


    public string? Remarks { get; init; }
}