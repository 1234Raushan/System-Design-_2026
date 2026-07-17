using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.Students.Commands.CreateStudent
{
    public sealed class CreateStudentValidator
        : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentValidator()
        {
            RuleFor(x => x.AdmissionNumber)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.RollNumber)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Gender)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Today);

            RuleFor(x => x.AdmissionDate)
                .NotEmpty();

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20);

            RuleFor(x => x.Address)
                .MaximumLength(500);
        }
    }
}
