using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Fees.Commands.UpdateFeePayment;

public sealed class UpdateFeePaymentHandler
    : IRequestHandler<UpdateFeePaymentCommand>
{
    private readonly IApplicationDbContext _context;


    public UpdateFeePaymentHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }



    public async Task Handle(
        UpdateFeePaymentCommand request,
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



        var feeAssignment =
            payment.FeeAssignment;



        // Remove old payment impact

        feeAssignment.AddPayment(
            -payment.PaidAmount);



        // Add new payment impact

        feeAssignment.AddPayment(
            request.PaidAmount);



        payment.Update(
            request.PaidAmount,
            request.PaymentDate,
            request.PaymentMode,
            request.TransactionNo,
            request.Remarks);



        await _context.SaveChangesAsync(
            cancellationToken);
    }
}