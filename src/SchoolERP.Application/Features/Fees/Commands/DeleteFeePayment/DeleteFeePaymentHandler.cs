using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Fees.Commands.DeleteFeePayment;

public sealed class DeleteFeePaymentHandler
    : IRequestHandler<DeleteFeePaymentCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteFeePaymentHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteFeePaymentCommand request,
        CancellationToken cancellationToken)
    {

        var payment =
            await _context.FeePayments
            .Include(x => x.FeeAssignment)
            .FirstOrDefaultAsync(x =>
                x.Id == request.FeePaymentId &&
                !x.IsDeleted,
                cancellationToken);

        if (payment is null)
        {
            throw new InvalidOperationException(
                "Fee payment not found.");
        }
        // Reverse Payment Amount

        payment.FeeAssignment
            .AddPayment(
                -payment.PaidAmount);
        // Soft Delete Payment

        payment.SoftDelete();

        // Receipt Soft Delete (if exists)

        var receipt =
            await _context.Receipts
            .FirstOrDefaultAsync(x =>
                x.FeePaymentId == payment.Id &&
                !x.IsDeleted,
                cancellationToken);

        if (receipt is not null)
        {
            receipt.SoftDelete();
        }
        await _context.SaveChangesAsync(
            cancellationToken);
    }
}