using MediatR;

namespace SchoolERP.Application.Features.TeacherClasses.Commands.AssignClass;

public sealed record AssignClassCommand : IRequest<int>
{
    public int TeacherId { get; init; }

    public int ClassId { get; init; }
}