using FluentValidation;

namespace SchoolERP.Application.Features.Fees.Commands.CreateFeeAssignment;

public sealed class CreateFeeAssignmentValidator
    : AbstractValidator<CreateFeeAssignmentCommand>
{
    public CreateFeeAssignmentValidator()
    {
        RuleFor(x => x.StudentEnrollmentId)
            .GreaterThan(0)
            .WithMessage("Student Enrollment is required.");


        RuleFor(x => x.AcademicSessionId)
            .GreaterThan(0)
            .WithMessage("Academic Session is required.");


        RuleFor(x => x.TotalAmount)
            .GreaterThan(0)
            .WithMessage("Fee amount must be greater than zero.");
    }
}