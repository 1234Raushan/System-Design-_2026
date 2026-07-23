using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Fees.Commands.CreateReceipt;

public sealed class CreateReceiptHandler
    : IRequestHandler<CreateReceiptCommand, int>
{
    private readonly IApplicationDbContext _context;


    public CreateReceiptHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }



    public async Task<int> Handle(
        CreateReceiptCommand request,
        CancellationToken cancellationToken)
    {

        // Check Payment

        var payment =
            await _context.FeePayments
            .FirstOrDefaultAsync(x =>
                x.Id == request.FeePaymentId &&
                !x.IsDeleted,
                cancellationToken);



        if (payment is null)
        {
            throw new InvalidOperationException(
                "Fee payment not found.");
        }



        // Duplicate Receipt Check

        var alreadyExists =
            await _context.Receipts
            .AnyAsync(x =>
                x.FeePaymentId == request.FeePaymentId &&
                !x.IsDeleted,
                cancellationToken);



        if (alreadyExists)
        {
            throw new InvalidOperationException(
                "Receipt already generated.");
        }



        // Generate Receipt Number

        var receiptNumber =
            $"REC-{DateTime.UtcNow.Year}-{request.FeePaymentId:D6}";



        var receipt = new Receipt(
            request.FeePaymentId,
            receiptNumber,
            DateOnly.FromDateTime(DateTime.UtcNow),
            payment.PaidAmount);



        _context.Receipts.Add(receipt);



        await _context.SaveChangesAsync(
            cancellationToken);



        return receipt.Id;
    }
}