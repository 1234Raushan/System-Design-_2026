using MediatR;

namespace SchoolERP.Application.Features.Exams.Commands.DeleteExam;

public sealed record DeleteExamCommand(int Id)
    : IRequest;