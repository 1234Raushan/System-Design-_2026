using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Fees.Commands.CreateFeePayment;

public sealed class CreateFeePaymentHandler
    : IRequestHandler<CreateFeePaymentCommand, int>
{
    private readonly IApplicationDbContext _context;


    public CreateFeePaymentHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }



    public async Task<int> Handle(
        CreateFeePaymentCommand request,
        CancellationToken cancellationToken)
    {

        var feeAssignment =
            await _context.FeeAssignments
            .FirstOrDefaultAsync(x =>
                x.Id == request.FeeAssignmentId &&
                !x.IsDeleted,
                cancellationToken);



        if (feeAssignment is null)
        {
            throw new InvalidOperationException(
                "Fee assignment not found.");
        }



        if (request.PaidAmount > feeAssignment.DueAmount)
        {
            throw new InvalidOperationException(
                "Payment amount cannot be greater than due amount.");
        }



        var payment = new FeePayment(
            request.FeeAssignmentId,
            request.PaidAmount,
            request.PaymentDate,
            request.PaymentMode,
            request.TransactionNo,
            request.Remarks);



        _context.FeePayments.Add(payment);



        // Update Fee Assignment

        feeAssignment.AddPayment(
            request.PaidAmount);



        await _context.SaveChangesAsync(
            cancellationToken);



        return payment.Id;
    }
}