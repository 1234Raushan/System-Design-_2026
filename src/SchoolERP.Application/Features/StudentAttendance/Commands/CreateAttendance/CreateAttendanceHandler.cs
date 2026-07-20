using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Attendance.Commands.CreateAttendance;

public sealed class CreateAttendanceHandler:IRequestHandler<CreateAttendanceCommand, int>
{

    private readonly IApplicationDbContext _context;
    public CreateAttendanceHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(
        CreateAttendanceCommand request,
        CancellationToken cancellationToken)
    {
        // Duplicate attendance check

        var alreadyExists =
            await _context.AttendanceSessions
            .AnyAsync(x =>
                x.AcademicSessionId == request.AcademicSessionId &&
                x.ClassId == request.ClassId &&
                x.SectionId == request.SectionId &&
                x.AttendanceDate == request.AttendanceDate &&
                !x.IsDeleted,
                cancellationToken);
        if (alreadyExists)
        {
            throw new InvalidOperationException(
                "Attendance already marked.");
        }
        // Create Attendance Session

        var attendanceSession =
            new AttendanceSession(
                request.AcademicSessionId,
                request.ClassId,
                request.SectionId,
                request.AttendanceDate);
        _context.AttendanceSessions
            .Add(attendanceSession);
        await _context.SaveChangesAsync(
            cancellationToken);
        // Add Students Attendance

        foreach (var item in request.Students)
        {
            var attendance =
                new StudentAttendance(
                    attendanceSession.Id,
                    item.StudentId,
                    item.Status,
                    item.Remarks);
            _context.StudentAttendances
                .Add(attendance);

        }
        await _context.SaveChangesAsync(
            cancellationToken);
        return attendanceSession.Id;
    }
}