using SchoolERP.Domain.Common;

namespace SchoolERP.Domain.Entities;

public sealed class Mark : BaseAuditableEntity
{
    public int ExamId { get; private set; }

    public int StudentEnrollmentId { get; private set; }

    public decimal ObtainedMarks { get; private set; }

    public string? Remarks { get; private set; }

    // Navigation

    public Exam Exam { get; private set; } = null!;

    public StudentEnrollment StudentEnrollment { get; private set; } = null!;

    private Mark()
    {
    }

    public Mark(
        int examId,
        int studentEnrollmentId,
        decimal obtainedMarks,
        string? remarks)
    {
        ExamId = examId;
        StudentEnrollmentId = studentEnrollmentId;
        ObtainedMarks = obtainedMarks;
        Remarks = remarks;
    }

    public void Update(
        decimal obtainedMarks,
        string? remarks,
        bool isActive)
    {
        ObtainedMarks = obtainedMarks;
        Remarks = remarks;

        if (isActive)
            Activate();
        else
            Deactivate();

        MarkAsUpdated();
    }
}