using MediatR;

namespace SchoolERP.Application.Features.Timetables.Commands.UpdateTimetable;

public sealed record UpdateTimetableCommand : IRequest<int>
{
    public int Id { get; init; }

    public int TeachingAssignmentId { get; init; }

    public DayOfWeek DayOfWeek { get; init; }

    public int PeriodNumber { get; init; }

    public TimeOnly StartTime { get; init; }

    public TimeOnly EndTime { get; init; }

    public string? RoomNumber { get; init; }

    public string? Remarks { get; init; }

    public bool IsActive { get; init; }
}