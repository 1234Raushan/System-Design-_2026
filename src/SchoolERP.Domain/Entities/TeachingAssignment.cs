using SchoolERP.Domain.Common;

namespace SchoolERP.Domain.Entities;

public sealed class TeachingAssignment : BaseAuditableEntity
{
    public int TeacherId { get; private set; }

    public int SubjectId { get; private set; }

    public int ClassId { get; private set; }

    public int SectionId { get; private set; }

    // Navigation Properties
    public Teacher Teacher { get; private set; } = null!;

    public Subject Subject { get; private set; } = null!;

    public Class_A Class { get; private set; } = null!;

    public Section Section { get; private set; } = null!;

    public ICollection<Timetable> Timetables { get; private set; }
        = new List<Timetable>();

    private TeachingAssignment()
    {
    }

    public TeachingAssignment(
        int teacherId,
        int subjectId,
        int classId,
        int sectionId)
    {
        TeacherId = teacherId;
        SubjectId = subjectId;
        ClassId = classId;
        SectionId = sectionId;
    }

    public void Update(
        int teacherId,
        int subjectId,
        int classId,
        int sectionId,
        bool isActive)
    {
        TeacherId = teacherId;
        SubjectId = subjectId;
        ClassId = classId;
        SectionId = sectionId;

        if (isActive)
            Activate();
        else
            Deactivate();

        MarkAsUpdated();
    }
}