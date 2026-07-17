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

        public string RollNumber { get; private set; } = string.Empty;

        public string FirstName { get; private set; } = string.Empty;

        public string LastName { get; private set; } = string.Empty;

        public DateTime DateOfBirth { get; private set; }

        public string Gender { get; private set; } = string.Empty;

        public string? Email { get; private set; }

        public string? PhoneNumber { get; private set; }

        public string? Address { get; private set; }

        public DateTime AdmissionDate { get; private set; }

        // Temporary (FKs will be configured later)
        public int? ClassId { get; private set; }

        public int? SectionId { get; private set; }

        // EF Core
        private Student()
        {
        }

        public Student(
            string admissionNumber,
            string rollNumber,
            string firstName,
            string lastName,
            DateTime dateOfBirth,
            string gender,
            DateTime admissionDate,
            string? email = null,
            string? phoneNumber = null,
            string? address = null)
        {
            SetAdmissionNumber(admissionNumber);
            SetRollNumber(rollNumber);
            UpdateName(firstName, lastName);

            DateOfBirth = dateOfBirth;
            Gender = gender.Trim();

            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;

            AdmissionDate = admissionDate;

            CreatedDate = DateTime.UtcNow;
        }

        public void SetAdmissionNumber(string admissionNumber)
        {
            if (string.IsNullOrWhiteSpace(admissionNumber))
                throw new ArgumentException("Admission Number is required.");

            AdmissionNumber = admissionNumber.Trim();
        }

        public void SetRollNumber(string rollNumber)
        {
            if (string.IsNullOrWhiteSpace(rollNumber))
                throw new ArgumentException("Roll Number is required.");

            RollNumber = rollNumber.Trim();
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

        public void AssignClass(int? classId)
        {
            ClassId = classId;
        }

        public void AssignSection(int? sectionId)
        {
            SectionId = sectionId;
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
        string rollNumber,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        string gender,
        DateTime admissionDate,
        string? email,
        string? phoneNumber,
        string? address,
        int? classId,
        int? sectionId,
        bool isActive)
        {
            AdmissionNumber = admissionNumber;
            RollNumber = rollNumber;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            AdmissionDate = admissionDate;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            ClassId = classId;
            SectionId = sectionId;
            IsActive = isActive;

            UpdatedDate = DateTime.UtcNow;
        }
    }
}
