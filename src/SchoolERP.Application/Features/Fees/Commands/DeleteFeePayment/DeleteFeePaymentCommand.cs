using MediatR;

namespace SchoolERP.Application.Features.Fees.Commands.DeleteFeePayment;

public sealed record DeleteFeePaymentCommand(
    int FeePaymentId
) : IRequest;