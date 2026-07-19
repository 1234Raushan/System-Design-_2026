using MediatR;

namespace SchoolERP.Application.Features.Subjects.Commands.DeleteSubject;

public sealed record DeleteSubjectCommand(int Id) : IRequest;