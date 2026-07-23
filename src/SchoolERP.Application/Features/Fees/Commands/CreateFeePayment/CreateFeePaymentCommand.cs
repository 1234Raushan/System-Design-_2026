using MediatR;

namespace SchoolERP.Application.Features.Fees.Commands.CreateFeePayment;

public sealed record CreateFeePaymentCommand
    : IRequest<int>
{
    public int FeeAssignmentId { get; init; }

    public decimal PaidAmount { get; init; }

    public DateOnly PaymentDate { get; init; }

    public string PaymentMode { get; init; }
        = string.Empty;

    public string? TransactionNo { get; init; }

    public string? Remarks { get; init; }
}