using FluentValidation;

namespace SchoolERP.Application.Features.StudentEnrollments.Commands.CreateEnrollment;

public sealed class CreateEnrollmentValidator
    : AbstractValidator<CreateEnrollmentCommand>
{
    public CreateEnrollmentValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0)
            .WithMessage("Student is required.");

        RuleFor(x => x.AcademicSessionId)
            .GreaterThan(0)
            .WithMessage("Academic Session is required.");

        RuleFor(x => x.ClassId)
            .GreaterThan(0)
            .WithMessage("Class is required.");

        RuleFor(x => x.SectionId)
            .GreaterThan(0)
            .WithMessage("Section is required.");

        RuleFor(x => x.RollNumber)
            .GreaterThan(0)
            .WithMessage("Roll Number must be greater than zero.");

        RuleFor(x => x.AdmissionDate)
            .NotEmpty()
            .WithMessage("Admission Date is required.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid Enrollment Status.");

        RuleFor(x => x.Remarks)
            .MaximumLength(500)
            .WithMessage("Remarks cannot exceed 500 characters.");
    }
}