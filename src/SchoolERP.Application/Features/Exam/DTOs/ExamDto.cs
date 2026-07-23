namespace SchoolERP.Application.Features.Exams.DTOs;

public sealed record ExamDto
{
    public int Id { get; init; }

    public int TeachingAssignmentId { get; init; }

    public string TeacherName { get; init; } = string.Empty;

    public string SubjectName { get; init; } = string.Empty;

    public string ClassName { get; init; } = string.Empty;

    public string SectionName { get; init; } = string.Empty;

    public string ExamName { get; init; } = string.Empty;

    public string ExamType { get; init; } = string.Empty;

    public DateOnly ExamDate { get; init; }

    public decimal MaximumMarks { get; init; }

    public decimal PassingMarks { get; init; }

    public bool IsActive { get; init; }
}