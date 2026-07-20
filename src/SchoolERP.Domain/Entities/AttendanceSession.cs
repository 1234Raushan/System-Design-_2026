using SchoolERP.Domain.Common;

namespace SchoolERP.Domain.Entities;

public sealed class AttendanceSession : BaseAuditableEntity
{
    public int AcademicSessionId { get; private set; }

    public int ClassId { get; private set; }

    public int SectionId { get; private set; }

    public DateOnly AttendanceDate { get; private set; }


    // Navigation

    public AcademicSession AcademicSession { get; private set; } = null!;

    public Class_A Class { get; private set; } = null!;

    public Section Section { get; private set; } = null!;


    public ICollection<StudentAttendance> StudentAttendances
    { get; private set; }
        = new List<StudentAttendance>();


    private AttendanceSession()
    {
    }


    public AttendanceSession(
        int academicSessionId,
        int classId,
        int sectionId,
        DateOnly attendanceDate)
    {
        AcademicSessionId = academicSessionId;
        ClassId = classId;
        SectionId = sectionId;
        AttendanceDate = attendanceDate;

        CreatedDate = DateTime.UtcNow;
    }
}