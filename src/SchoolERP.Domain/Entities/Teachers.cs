using SchoolERP.Domain.Common;

namespace SchoolERP.Domain.Entities;

public sealed class Teacher : BaseAuditableEntity
{
    public int UserId { get; private set; }
    public string EmployeeCode { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string? PhoneNumber { get; private set; }
    public string? Gender { get; private set; }

    public DateTime? DateOfBirth { get; private set; }

    public DateTime JoiningDate { get; private set; }

    public string? Qualification { get; private set; }

    public int? Experience { get; private set; }

    public string? Address { get; private set; }

    // Navigation
    public User User { get; private set; } = null!;

    public ICollection<TeacherSubject> TeacherSubjects { get; private set; }
    = new List<TeacherSubject>();

    public ICollection<TeacherClass> TeacherClasses { get; private set; }
    = new List<TeacherClass>();
    public ICollection<TeachingAssignment> TeachingAssignments { get; private set; }
    = new List<TeachingAssignment>();

    private Teacher()
    {
    }

    public Teacher(
        int userId,
        string employeeCode,
        string firstName,
        string lastName,
        string email,
        string? phoneNumber,
        string? gender,
        DateTime? dateOfBirth,
        DateTime joiningDate,
        string? qualification,
        int? experience,
        string? address)
    {
        UserId = userId;
        EmployeeCode = employeeCode.Trim().ToUpperInvariant();
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim().ToLowerInvariant();
        PhoneNumber = phoneNumber;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        JoiningDate = joiningDate;
        Qualification = qualification;
        Experience = experience;
        Address = address;
    }

    public void Update(
        string firstName,
        string lastName,
        string email,
        string? phoneNumber,
        string? gender,
        DateTime? dateOfBirth,
        string? qualification,
        int? experience,
        string? address,
        bool isActive)
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim().ToLowerInvariant();

        PhoneNumber = phoneNumber;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Qualification = qualification;
        Experience = experience;
        Address = address;

        if (isActive)
            Activate();
        else
            Deactivate();

        MarkAsUpdated();
    }
}