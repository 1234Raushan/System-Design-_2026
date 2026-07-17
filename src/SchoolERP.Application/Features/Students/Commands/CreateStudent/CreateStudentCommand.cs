using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.Students.Commands.CreateStudent
{
    public sealed record CreateStudentCommand : IRequest<int>
    {
        public string AdmissionNumber { get; init; } = string.Empty;

        public string RollNumber { get; init; } = string.Empty;

        public string FirstName { get; init; } = string.Empty;

        public string LastName { get; init; } = string.Empty;

        public DateTime DateOfBirth { get; init; }

        public string Gender { get; init; } = string.Empty;

        public string? Email { get; init; }

        public string? PhoneNumber { get; init; }

        public string? Address { get; init; }

        public DateTime AdmissionDate { get; init; }

        public int? ClassId { get; init; }

        public int? SectionId { get; init; }
    }
}
