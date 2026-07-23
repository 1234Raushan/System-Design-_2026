namespace SchoolERP.Application.Features.Exams.DTOs;

public sealed record UpdateExamRequest
{
    public int Id { get; init; }

    public string ExamName { get; init; } = string.Empty;

    public string ExamType { get; init; } = string.Empty;

    public DateOnly ExamDate { get; init; }

    public decimal MaximumMarks { get; init; }

    public decimal PassingMarks { get; init; }

    public string? Description { get; init; }

    public bool IsActive { get; init; }
}