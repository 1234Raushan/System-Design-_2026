using SchoolERP.Domain.Common;

namespace SchoolERP.Domain.Entities;

public sealed class Timetable : BaseAuditableEntity
{
    public int TeachingAssignmentId { get; private set; }

    public DayOfWeek DayOfWeek { get; private set; }

    public int PeriodNumber { get; private set; }

    public TimeOnly StartTime { get; private set; }

    public TimeOnly EndTime { get; private set; }

    public string? RoomNumber { get; private set; }

    public string? Remarks { get; private set; }


    // Navigation Property
    public TeachingAssignment TeachingAssignment { get; private set; } = null!;


    private Timetable()
    {
    }


    public Timetable(
        int teachingAssignmentId,
        DayOfWeek dayOfWeek,
        int periodNumber,
        TimeOnly startTime,
        TimeOnly endTime,
        string? roomNumber,
        string? remarks)
    {
        TeachingAssignmentId = teachingAssignmentId;

        DayOfWeek = dayOfWeek;

        PeriodNumber = periodNumber;

        StartTime = startTime;

        EndTime = endTime;

        RoomNumber = roomNumber?.Trim();

        Remarks = remarks?.Trim();
    }


    public void Update(
        int teachingAssignmentId,
        DayOfWeek dayOfWeek,
        int periodNumber,
        TimeOnly startTime,
        TimeOnly endTime,
        string? roomNumber,
        string? remarks,
        bool isActive)
    {
        TeachingAssignmentId = teachingAssignmentId;

        DayOfWeek = dayOfWeek;

        PeriodNumber = periodNumber;

        StartTime = startTime;

        EndTime = endTime;

        RoomNumber = roomNumber?.Trim();

        Remarks = remarks?.Trim();


        if (isActive)
            Activate();
        else
            Deactivate();


        MarkAsUpdated();
    }
}