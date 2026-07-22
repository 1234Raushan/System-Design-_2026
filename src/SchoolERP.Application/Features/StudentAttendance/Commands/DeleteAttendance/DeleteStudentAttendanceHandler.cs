using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.StudentAttendance.Commands.DeleteAttendance;

public sealed class DeleteStudentAttendanceHandler
    : IRequestHandler<DeleteStudentAttendanceCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteStudentAttendanceHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteStudentAttendanceCommand request,
        CancellationToken cancellationToken)
    {
        // Attendance Session

        var attendanceSession =
            await _context.AttendanceSessions
            .FirstOrDefaultAsync(x =>
                x.Id == request.AttendanceSessionId &&
                !x.IsDeleted,
                cancellationToken);

        if (attendanceSession is null)
        {
            throw new InvalidOperationException(
                "Attendance session not found.");
        }

        // Student Attendance List

        var attendances =
            await _context.StudentAttendances
            .Where(x =>
                x.AttendanceSessionId == request.AttendanceSessionId &&
                !x.IsDeleted)
            .ToListAsync(cancellationToken);

        // Soft Delete Student Attendance

        foreach (var attendance in attendances)
        {
            attendance.SoftDelete();
        }

        // Soft Delete Attendance Session

        attendanceSession.SoftDelete();

        await _context.SaveChangesAsync(cancellationToken);
    }
}