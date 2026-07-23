using MediatR;

namespace SchoolERP.Application.Features.Exams.Queries.GetExamById;

public sealed record GetExamByIdQuery(int Id)
    : IRequest<ExamDetailsDto>;

public sealed class ExamDetailsDto
{
    public int Id { get; set; }

    public int TeachingAssignmentId { get; set; }

    public string TeacherName { get; set; } = string.Empty;

    public string SubjectName { get; set; } = string.Empty;

    public string ClassName { get; set; } = string.Empty;

    public string SectionName { get; set; } = string.Empty;

    public string ExamName { get; set; } = string.Empty;

    public string ExamType { get; set; } = string.Empty;

    public DateOnly ExamDate { get; set; }

    public decimal MaximumMarks { get; set; }

    public decimal PassingMarks { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}