using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Timetables.Commands.DeleteTimetable;

public sealed class DeleteTimetableHandler
    : IRequestHandler<DeleteTimetableCommand, int>
{
    private readonly IApplicationDbContext _context;

    public DeleteTimetableHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        DeleteTimetableCommand request,
        CancellationToken cancellationToken)
    {
        var timetable = await _context.Timetables
            .FirstOrDefaultAsync(x => x.Id == request.Id &&
                                      !x.IsDeleted,
                cancellationToken);

        if (timetable is null)
            throw new InvalidOperationException("Timetable not found.");

        timetable.SoftDelete();

        await _context.SaveChangesAsync(cancellationToken);

        return timetable.Id;
    }
}