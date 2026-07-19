using MediatR;

namespace SchoolERP.Application.Features.TeacherSubjects.Commands.RemoveSubject;

public sealed record RemoveSubjectCommand(int Id) : IRequest;