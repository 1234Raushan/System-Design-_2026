using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.Students.DTOs
{
    public sealed class StudentDto
    {
        public int Id { get; init; }

        public string AdmissionNumber { get; init; } = string.Empty;

        public string RollNumber { get; init; } = string.Empty;

        public string FirstName { get; init; } = string.Empty;

        public string LastName { get; init; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

        public DateTime DateOfBirth { get; init; }

        public string Gender { get; init; } = string.Empty;

        public string? Email { get; init; }

        public string? PhoneNumber { get; init; }

        public string? Address { get; init; }

        public DateTime AdmissionDate { get; init; }

        public int? ClassId { get; init; }

        public int? SectionId { get; init; }

        public bool IsActive { get; init; }
    }
}
