using MediatR;

namespace SchoolERP.Application.Features.Fees.Commands.CreateReceipt;

public sealed record CreateReceiptCommand
    : IRequest<int>
{
    public int FeePaymentId { get; init; }
}