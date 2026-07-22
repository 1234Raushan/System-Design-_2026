using MediatR;

namespace SchoolERP.Application.Features.StudentAttendance.Commands.DeleteAttendance;

public sealed record DeleteStudentAttendanceCommand(int AttendanceSessionId)
    : IRequest;