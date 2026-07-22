using MediatR;

namespace SchoolERP.Application.Features.TeachingAssignments.Commands.DeleteTeachingAssignment;

public sealed record DeleteTeachingAssignmentCommand(
    int Id
) : IRequest;