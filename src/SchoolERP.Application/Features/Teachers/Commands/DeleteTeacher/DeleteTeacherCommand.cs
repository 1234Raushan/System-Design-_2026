using MediatR;

namespace SchoolERP.Application.Features.Teachers.Commands.DeleteTeacher;

public sealed record DeleteTeacherCommand(int Id) : IRequest;