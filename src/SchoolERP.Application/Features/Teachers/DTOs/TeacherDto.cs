namespace SchoolERP.Application.Features.Teachers.DTOs;

public sealed class TeacherDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string EmployeeCode { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    public string Email { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public DateTime JoiningDate { get; set; }

    public string? Qualification { get; set; }

    public int? Experience { get; set; }

    public string? Address { get; set; }

    public bool IsActive { get; set; }
}