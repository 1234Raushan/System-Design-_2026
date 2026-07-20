using SchoolERP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Domain.Entities
{
    public sealed class Student : BaseAuditableEntity
    {
        public string AdmissionNumber { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public string Gender { get; private set; } = string.Empty;
        public string? Email { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Address { get; private set; }
        public ICollection<StudentEnrollment> Enrollments { get; private set; }
        = new List<StudentEnrollment>();

        // EF Core
        private Student()
        {
        }

        public Student(
            string admissionNumber,
            string firstName,
            string lastName,
            DateTime dateOfBirth,
            string gender,
            string? email = null,
            string? phoneNumber = null,
            string? address = null)
        {
            SetAdmissionNumber(admissionNumber);
            UpdateName(firstName, lastName);
            DateOfBirth = dateOfBirth;
            Gender = gender.Trim();
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            CreatedDate = DateTime.UtcNow;
        }

        public void SetAdmissionNumber(string admissionNumber)
        {
            if (string.IsNullOrWhiteSpace(admissionNumber))
                throw new ArgumentException("Admission Number is required.");

            AdmissionNumber = admissionNumber.Trim();
        }

        public void UpdateName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First Name is required.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last Name is required.");

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
        }
        public void UpdateContact(
            string? email,
            string? phoneNumber,
            string? address)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }
        public void Update(
        string admissionNumber,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        string gender,
        string? email,
        string? phoneNumber,
        string? address,
        bool isActive)
        {
            AdmissionNumber = admissionNumber;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            IsActive = isActive;
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
