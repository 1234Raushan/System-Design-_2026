using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Timetables.Commands.UpdateTimetable;

public sealed class UpdateTimetableHandler
    : IRequestHandler<UpdateTimetableCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdateTimetableHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<int> Handle(
        UpdateTimetableCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Timetable should exist
        var timetable = await _context.Timetables
            .FirstOrDefaultAsync(x =>
                x.Id == request.Id &&
                !x.IsDeleted,
                cancellationToken);


        if (timetable is null)
            throw new InvalidOperationException(
                "Timetable not found.");


        // 2. Teaching Assignment should exist
        var teachingAssignmentExists = await _context.TeachingAssignments
            .AnyAsync(x =>
                x.Id == request.TeachingAssignmentId &&
                !x.IsDeleted,
                cancellationToken);


        if (!teachingAssignmentExists)
            throw new InvalidOperationException(
                "Teaching Assignment not found.");


        // 3. Duplicate timetable validation
        var timetableExists = await _context.Timetables
            .AnyAsync(x =>
                x.Id != request.Id &&
                x.TeachingAssignmentId == request.TeachingAssignmentId &&
                x.DayOfWeek == request.DayOfWeek &&
                x.PeriodNumber == request.PeriodNumber &&
                !x.IsDeleted,
                cancellationToken);


        if (timetableExists)
            throw new InvalidOperationException(
                "Timetable already exists for this period.");


        // 4. End Time validation
        if (request.EndTime <= request.StartTime)
            throw new InvalidOperationException(
                "End Time must be greater than Start Time.");


        // 5. Update Entity
        timetable.Update(
            request.TeachingAssignmentId,
            request.DayOfWeek,
            request.PeriodNumber,
            request.StartTime,
            request.EndTime,
            request.RoomNumber,
            request.Remarks,
            request.IsActive);


        await _context.SaveChangesAsync(cancellationToken);


        return timetable.Id;
    }
}