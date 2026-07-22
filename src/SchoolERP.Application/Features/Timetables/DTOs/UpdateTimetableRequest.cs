namespace SchoolERP.Application.Features.Timetables.DTOs;

public sealed class UpdateTimetableRequest
{
    public int TeachingAssignmentId { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public int PeriodNumber { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public string? RoomNumber { get; set; }

    public string? Remarks { get; set; }

    public bool IsActive { get; set; }
}