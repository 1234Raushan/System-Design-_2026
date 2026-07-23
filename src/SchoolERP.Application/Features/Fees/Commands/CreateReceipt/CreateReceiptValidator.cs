using FluentValidation;

namespace SchoolERP.Application.Features.Fees.Commands.CreateReceipt;

public sealed class CreateReceiptValidator
    : AbstractValidator<CreateReceiptCommand>
{
    public CreateReceiptValidator()
    {
        RuleFor(x => x.FeePaymentId)
            .GreaterThan(0)
            .WithMessage("Fee Payment is required.");
    }
}