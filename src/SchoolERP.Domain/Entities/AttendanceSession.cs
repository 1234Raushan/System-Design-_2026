using SchoolERP.Domain.Common;

namespace SchoolERP.Domain.Entities;

public sealed class AttendanceSession : BaseAuditableEntity
{
    public int TeachingAssignmentId { get; private set; }

    public DateOnly AttendanceDate { get; private set; }

    // Navigation
    public TeachingAssignment TeachingAssignment { get; private set; } = null!;

    public ICollection<Student_Attendance> StudentAttendances
    { get; private set; }
        = new List<Student_Attendance>();

    private AttendanceSession()
    {
    }

    public AttendanceSession(
        int teachingAssignmentId,
        DateOnly attendanceDate)
    {
        TeachingAssignmentId = teachingAssignmentId;
        AttendanceDate = attendanceDate;
    }

    public void Update(DateOnly attendanceDate, bool isActive)
    {
        AttendanceDate = attendanceDate;

        if (isActive)
            Activate();
        else
            Deactivate();

        MarkAsUpdated();
    }
}