using MediatR;

namespace SchoolERP.Application.Features.Timetables.Commands.CreateTimetable;

public sealed record CreateTimetableCommand : IRequest<int>
{
    public int TeachingAssignmentId { get; init; }

    public DayOfWeek DayOfWeek { get; init; }

    public int PeriodNumber { get; init; }

    public TimeOnly StartTime { get; init; }

    public TimeOnly EndTime { get; init; }

    public string? RoomNumber { get; init; }

    public string? Remarks { get; init; }
}