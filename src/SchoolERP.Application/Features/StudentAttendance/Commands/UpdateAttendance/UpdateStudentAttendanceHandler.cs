using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.StudentAttendance.Commands.UpdateAttendance;

public sealed class UpdateStudentAttendanceHandler
    : IRequestHandler<UpdateStudentAttendanceCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateStudentAttendanceHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateStudentAttendanceCommand request,
        CancellationToken cancellationToken)
    {
        // Check Attendance Session

        var attendanceSession = await _context.AttendanceSessions
            .FirstOrDefaultAsync(x =>
                x.Id == request.AttendanceSessionId &&
                !x.IsDeleted,
                cancellationToken);

        if (attendanceSession is null)
        {
            throw new InvalidOperationException(
                "Attendance session not found.");
        }

        // Load Attendance Records

        var attendances = await _context.StudentAttendances
            .Where(x =>
                x.AttendanceSessionId == request.AttendanceSessionId &&
                !x.IsDeleted)
            .ToListAsync(cancellationToken);

        // Dictionary for O(1) Lookup

        var attendanceDictionary = attendances
            .ToDictionary(x => x.StudentEnrollmentId);

        // Update Attendance

        foreach (var item in request.Students)
        {
            if (!attendanceDictionary.TryGetValue(
                    item.StudentEnrollmentId,
                    out var attendance))
            {
                throw new InvalidOperationException(
                    $"Attendance not found for Student Enrollment Id : {item.StudentEnrollmentId}");
            }

            attendance.Update(
                item.Status,
                item.Remarks);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}