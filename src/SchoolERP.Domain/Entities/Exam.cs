using SchoolERP.Domain.Common;

namespace SchoolERP.Domain.Entities;

public sealed class Exam : BaseAuditableEntity
{
    public int TeachingAssignmentId { get; private set; }

    public string ExamName { get; private set; } = string.Empty;

    public string ExamType { get; private set; } = string.Empty;

    public DateOnly ExamDate { get; private set; }

    public decimal MaximumMarks { get; private set; }

    public decimal PassingMarks { get; private set; }

    public string? Description { get; private set; }

    // Navigation
    public TeachingAssignment TeachingAssignment { get; private set; } = null!;

    public ICollection<Mark> Marks { get; private set; }
        = new List<Mark>();

    private Exam()
    {
    }

    public Exam(
        int teachingAssignmentId,
        string examName,
        string examType,
        DateOnly examDate,
        decimal maximumMarks,
        decimal passingMarks,
        string? description)
    {
        TeachingAssignmentId = teachingAssignmentId;
        ExamName = examName;
        ExamType = examType;
        ExamDate = examDate;
        MaximumMarks = maximumMarks;
        PassingMarks = passingMarks;
        Description = description;
    }

    public void Update(
        string examName,
        string examType,
        DateOnly examDate,
        decimal maximumMarks,
        decimal passingMarks,
        string? description,
        bool isActive)
    {
        ExamName = examName;
        ExamType = examType;
        ExamDate = examDate;
        MaximumMarks = maximumMarks;
        PassingMarks = passingMarks;
        Description = description;

        if (isActive)
            Activate();
        else
            Deactivate();

        MarkAsUpdated();
    }
}