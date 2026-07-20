namespace SchoolERP.Application.Features.StudentEnrollments.DTOs;

public sealed class EnrollmentDto
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public string StudentName { get; set; } = string.Empty;

    public int AcademicSessionId { get; set; }

    public string AcademicSession { get; set; } = string.Empty;

    public int ClassId { get; set; }

    public string ClassName { get; set; } = string.Empty;

    public int SectionId { get; set; }

    public string SectionName { get; set; } = string.Empty;

    public int RollNumber { get; set; }

    public DateOnly AdmissionDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public string? Remarks { get; set; }

    public bool IsActive { get; set; }
}