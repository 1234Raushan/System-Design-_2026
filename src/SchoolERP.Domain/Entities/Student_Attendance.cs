using SchoolERP.Domain.Common;
using SchoolERP.Domain.Enums;

namespace SchoolERP.Domain.Entities;

public sealed class Student_Attendance : BaseAuditableEntity
{
    public int AttendanceSessionId { get; private set; }
    public int StudentEnrollmentId { get; private set; }
    public AttendanceStatus Status { get; private set; }
    public string? Remarks { get; private set; }
    // Navigation
    public AttendanceSession AttendanceSession { get; private set; } = null!;
    public StudentEnrollment StudentEnrollment { get; private set; } = null!;
    private Student_Attendance()
    {
    }
    public Student_Attendance(
        int attendanceSessionId,
        int studentEnrollmentId,
        AttendanceStatus status,
        string? remarks)
    {
        AttendanceSessionId = attendanceSessionId;
        StudentEnrollmentId = studentEnrollmentId;
        Status = status;
        Remarks = remarks;
        CreatedDate = DateTime.UtcNow;
    }

    public void Update(
        AttendanceStatus status,
        string? remarks)
    {
        Status = status;
        Remarks = remarks;
        MarkAsUpdated();
    }
}