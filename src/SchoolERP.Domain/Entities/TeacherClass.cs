using SchoolERP.Domain.Common;

namespace SchoolERP.Domain.Entities;

public sealed class TeacherClass : BaseAuditableEntity
{
    public int TeacherId { get; private set; }

    public int ClassId { get; private set; }

    public Teacher Teacher { get; private set; } = null!;

    public Class_A Class { get; private set; } = null!;

    private TeacherClass()
    {

    }

    public TeacherClass(
        int teacherId,
        int classId)
    {
        TeacherId = teacherId;
        ClassId = classId;
    }
}