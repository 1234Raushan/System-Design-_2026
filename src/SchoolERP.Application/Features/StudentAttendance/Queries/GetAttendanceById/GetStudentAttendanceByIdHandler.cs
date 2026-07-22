using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.StudentAttendance.Queries.GetAttendanceById;

public sealed class GetStudentAttendanceByIdHandler
    : IRequestHandler<GetStudentAttendanceByIdQuery, AttendanceDetailsDto>
{
    private readonly IApplicationDbContext _context;

    public GetStudentAttendanceByIdHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AttendanceDetailsDto> Handle(
        GetStudentAttendanceByIdQuery request,
        CancellationToken cancellationToken)
    {
        var session = await _context.AttendanceSessions
            .AsNoTracking()
            .Where(x =>
                x.Id == request.AttendanceSessionId &&
                !x.IsDeleted)
            .Select(x => new AttendanceDetailsDto
            {
                AttendanceSessionId = x.Id,
                AcademicSessionId = x.AcademicSessionId,
                ClassId = x.ClassId,
                SectionId = x.SectionId,
                AttendanceDate = x.AttendanceDate,

                Students = x.StudentAttendances
                    .Where(a => !a.IsDeleted)
                    .Select(a => new StudentAttendanceDetailsDto
                    {
                        StudentEnrollmentId = a.StudentEnrollmentId,
                        StudentId = a.StudentEnrollment.StudentId,
                        StudentName =
                            a.StudentEnrollment.Student.FirstName + " " +
                            a.StudentEnrollment.Student.LastName,
                        RollNumber = a.StudentEnrollment.RollNumber,
                        Status = a.Status,
                        Remarks = a.Remarks
                    })
                    .OrderBy(x => x.RollNumber)
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (session is null)
        {
            throw new InvalidOperationException(
                "Attendance not found.");
        }

        return session;
    }
}