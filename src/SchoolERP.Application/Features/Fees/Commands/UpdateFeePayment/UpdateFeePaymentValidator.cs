using FluentValidation;

namespace SchoolERP.Application.Features.Fees.Commands.UpdateFeePayment;

public sealed class UpdateFeePaymentValidator
    : AbstractValidator<UpdateFeePaymentCommand>
{
    public UpdateFeePaymentValidator()
    {
        RuleFor(x => x.FeePaymentId)
            .GreaterThan(0);


        RuleFor(x => x.PaidAmount)
            .GreaterThan(0);


        RuleFor(x => x.PaymentDate)
            .NotEmpty();


        RuleFor(x => x.PaymentMode)
            .NotEmpty()
            .MaximumLength(50);


        RuleFor(x => x.TransactionNo)
            .MaximumLength(100);


        RuleFor(x => x.Remarks)
            .MaximumLength(500);
    }
}