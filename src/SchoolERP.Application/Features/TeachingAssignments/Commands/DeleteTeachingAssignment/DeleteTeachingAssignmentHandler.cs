using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.TeachingAssignments.Commands.DeleteTeachingAssignment;

public sealed class DeleteTeachingAssignmentHandler
    : IRequestHandler<DeleteTeachingAssignmentCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTeachingAssignmentHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteTeachingAssignmentCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Find Teaching Assignment
        var teachingAssignment = await _context.TeachingAssignments
            .FirstOrDefaultAsync(x =>
                x.Id == request.Id &&
                !x.IsDeleted,
                cancellationToken);

        if (teachingAssignment is null)
            throw new InvalidOperationException(
                "Teaching Assignment not found.");

        // 2. Soft Delete
        teachingAssignment.SoftDelete();

        // 3. Save Changes
        await _context.SaveChangesAsync(cancellationToken);
    }
}