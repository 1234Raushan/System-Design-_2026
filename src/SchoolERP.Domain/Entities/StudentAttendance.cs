using SchoolERP.Domain.Common;
using SchoolERP.Domain.Enums;

namespace SchoolERP.Domain.Entities;

public sealed class StudentAttendance : BaseAuditableEntity
{
    public int AttendanceSessionId { get; private set; }
    public int StudentId { get; private set; }
    public AttendanceStatus Status { get; private set; }
    public string? Remarks { get; private set; }
    // Navigation

    public AttendanceSession AttendanceSession
    {
        get; private set;
    } = null!;

    public Student Student
    {
        get; private set;
    } = null!;
    private StudentAttendance()
    {
    }
    public StudentAttendance(
        int attendanceSessionId,
        int studentId,
        AttendanceStatus status,
        string? remarks)
    {
        AttendanceSessionId = attendanceSessionId;

        StudentId = studentId;

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