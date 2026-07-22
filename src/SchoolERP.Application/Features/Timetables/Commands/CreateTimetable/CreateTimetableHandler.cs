using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Timetables.Commands.CreateTimetable;

public sealed class CreateTimetableHandler
    : IRequestHandler<CreateTimetableCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTimetableHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateTimetableCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Teaching Assignment should exist
        var teachingAssignmentExists = await _context.TeachingAssignments
            .AnyAsync(x =>
                x.Id == request.TeachingAssignmentId &&
                !x.IsDeleted,
                cancellationToken);

        if (!teachingAssignmentExists)
            throw new InvalidOperationException("Teaching Assignment not found.");

        // 2. Timetable should be unique for Teaching Assignment + Day + Period
        var timetableExists = await _context.Timetables
            .AnyAsync(x =>
                x.TeachingAssignmentId == request.TeachingAssignmentId &&
                x.DayOfWeek == request.DayOfWeek &&
                x.PeriodNumber == request.PeriodNumber &&
                !x.IsDeleted,
                cancellationToken);

        if (timetableExists)
            throw new InvalidOperationException(
                "Timetable already exists for the selected day and period.");

        // 3. End Time should be greater than Start Time
        if (request.EndTime <= request.StartTime)
            throw new InvalidOperationException(
                "End Time must be greater than Start Time.");

        // 4. Create Timetable
        var timetable = new Timetable(
            request.TeachingAssignmentId,
            request.DayOfWeek,
            request.PeriodNumber,
            request.StartTime,
            request.EndTime,
            request.RoomNumber,
            request.Remarks);

        _context.Timetables.Add(timetable);

        // 5. Save Changes
        await _context.SaveChangesAsync(cancellationToken);

        return timetable.Id;
    }
}