using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.AcademicSessions.Commands.DeleteAcademicSession;

public sealed class DeleteAcademicSessionHandler
    : IRequestHandler<DeleteAcademicSessionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteAcademicSessionHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteAcademicSessionCommand request,
        CancellationToken cancellationToken)
    {
        var session = await _context.AcademicSessions
            .FirstOrDefaultAsync(
                x => x.Id == request.Id &&
                     !x.IsDeleted,
                cancellationToken);

        if (session is null)
            throw new KeyNotFoundException("Academic session not found.");

        // Business Rule:
        // Current session cannot be deleted.
        if (session.IsCurrent)
            throw new InvalidOperationException(
                "Current academic session cannot be deleted.");

        session.SoftDelete();

        await _context.SaveChangesAsync(cancellationToken);
    }
}