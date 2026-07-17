using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.Students.Commands.UpdateStudent
{
    public sealed class UpdateStudentValidator
        : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

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

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email));
        }
    }
}
