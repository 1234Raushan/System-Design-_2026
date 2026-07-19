using SchoolERP.Domain.Common;

namespace SchoolERP.Domain.Entities;

public sealed class TeacherSubject : BaseAuditableEntity
{
    public int TeacherId { get; private set; }

    public int SubjectId { get; private set; }

    public Teacher Teacher { get; private set; } = null!;

    public Subject Subject { get; private set; } = null!;

    private TeacherSubject()
    {
    }

    public TeacherSubject(
        int teacherId,
        int subjectId)
    {
        TeacherId = teacherId;
        SubjectId = subjectId;
    }
}