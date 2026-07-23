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
        // Teaching Assignment Exists
        var teachingAssignment = await _context.TeachingAssignments
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.Id == request.TeachingAssignmentId &&
                !x.IsDeleted,
                cancellationToken);

        if (teachingAssignment is null)
            throw new InvalidOperationException("Teaching Assignment not found.");

        // Duplicate Attendance Check
        var alreadyExists = await _context.AttendanceSessions
            .AnyAsync(x =>
                x.TeachingAssignmentId == request.TeachingAssignmentId &&
                x.AttendanceDate == request.AttendanceDate &&
                !x.IsDeleted,
                cancellationToken);

        if (alreadyExists)
            throw new InvalidOperationException("Attendance already marked.");

        var enrollmentIds = request.Students
            .Select(x => x.StudentEnrollmentId)
            .ToList();

        // Validate Student Enrollments
        var enrollments = await _context.StudentEnrollments
            .Where(x =>
                enrollmentIds.Contains(x.Id) &&
                x.ClassId == teachingAssignment.ClassId &&
                x.SectionId == teachingAssignment.SectionId &&
                !x.IsDeleted)
            .ToListAsync(cancellationToken);

        if (enrollments.Count != enrollmentIds.Count)
            throw new InvalidOperationException(
                "One or more student enrollments are invalid.");

        // Create Attendance Session
        var attendanceSession = new AttendanceSession(
            request.TeachingAssignmentId,
            request.AttendanceDate);

        _context.AttendanceSessions.Add(attendanceSession);

        await _context.SaveChangesAsync(cancellationToken);

        var attendances = request.Students
            .Select(x => new Student_Attendance(
                attendanceSession.Id,
                x.StudentEnrollmentId,
                x.Status,
                x.Remarks))
            .ToList();

        _context.StudentAttendances.AddRange(attendances);

        await _context.SaveChangesAsync(cancellationToken);

        return attendanceSession.Id;
    }
}