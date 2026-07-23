using FluentValidation;

namespace SchoolERP.Application.Features.Fees.Commands.CreateFeePayment;

public sealed class CreateFeePaymentValidator
    : AbstractValidator<CreateFeePaymentCommand>
{
    public CreateFeePaymentValidator()
    {
        RuleFor(x => x.FeeAssignmentId)
            .GreaterThan(0)
            .WithMessage("Fee Assignment is required.");


        RuleFor(x => x.PaidAmount)
            .GreaterThan(0)
            .WithMessage("Payment amount must be greater than zero.");


        RuleFor(x => x.PaymentDate)
            .NotEmpty()
            .WithMessage("Payment date is required.");


        RuleFor(x => x.PaymentMode)
            .NotEmpty()
            .MaximumLength(50);


        RuleFor(x => x.TransactionNo)
            .MaximumLength(100);


        RuleFor(x => x.Remarks)
            .MaximumLength(500);
    }
}