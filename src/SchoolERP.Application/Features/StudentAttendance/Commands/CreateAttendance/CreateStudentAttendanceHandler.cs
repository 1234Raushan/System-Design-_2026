using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;
namespace SchoolERP.Application.Features.StudentAttendance.Commands.CreateAttendance;

public sealed class CreateStudentAttendanceHandler
    : IRequestHandler<CreateStudentAttendanceCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateStudentAttendanceHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateStudentAttendanceCommand request,
        CancellationToken cancellationToken)
    {
        // Duplicate Attendance Session Check
        var alreadyExists = await _context.AttendanceSessions
            .AnyAsync(x =>
                x.AcademicSessionId == request.AcademicSessionId &&
                x.ClassId == request.ClassId &&
                x.SectionId == request.SectionId &&
                x.AttendanceDate == request.AttendanceDate &&
                !x.IsDeleted,
                cancellationToken);

        if (alreadyExists)
        {
            throw new InvalidOperationException("Attendance already marked.");
        }

        // Get all Student Enrollment Ids
        var enrollmentIds = request.Students
            .Select(selector: x => x.StudentEnrollmentId)
            .ToList();

        // Fetch all enrollments in ONE query
        var enrollments = await _context.StudentEnrollments
            .Where(x =>
                enrollmentIds.Contains(x.Id) &&
                x.AcademicSessionId == request.AcademicSessionId &&
                x.ClassId == request.ClassId &&
                x.SectionId == request.SectionId &&
                !x.IsDeleted)
            .ToListAsync(cancellationToken);

        // Validation (next step me aur improve karenge)
        if (enrollments.Count != enrollmentIds.Count)
        {
            throw new InvalidOperationException(
                "One or more student enrollments are invalid.");
        }

        // Create Attendance Session
        var attendanceSession = new AttendanceSession(
            request.AcademicSessionId,
            request.ClassId,
            request.SectionId,
            request.AttendanceDate);

        _context.AttendanceSessions.Add(attendanceSession);

        await _context.SaveChangesAsync(cancellationToken);

        var attendances = request.Students
            .Select(item => new Student_Attendance(
                attendanceSession.Id,
                item.StudentEnrollmentId,
                item.Status,
                item.Remarks))
            .ToList();

        _context.StudentAttendances.AddRange(attendances);

        await _context.SaveChangesAsync(cancellationToken);

        return attendanceSession.Id;
    }
}