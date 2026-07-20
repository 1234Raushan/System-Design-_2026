using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.AcademicSessions.Commands.UpdateAcademicSession;

public sealed class UpdateAcademicSessionHandler
    : IRequestHandler<UpdateAcademicSessionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateAcademicSessionHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateAcademicSessionCommand request,
        CancellationToken cancellationToken)
    {
        var session = await _context.AcademicSessions
            .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted,
                cancellationToken);

        if (session is null)
            throw new KeyNotFoundException("Academic session not found.");

        var duplicate = await _context.AcademicSessions
            .AnyAsync(x =>
                x.Id != request.Id &&
                x.SessionName == request.SessionName &&
                !x.IsDeleted,
                cancellationToken);

        if (duplicate)
            throw new InvalidOperationException("Academic session already exists.");

        if (request.IsCurrent)
        {
            var currentSessions = await _context.AcademicSessions
                .Where(x => x.IsCurrent && x.Id != request.Id && !x.IsDeleted)
                .ToListAsync(cancellationToken);

            foreach (var item in currentSessions)
            {
                item.RemoveCurrent();   // ya item.Update(...false...) agar RemoveCurrent() abhi nahi hai
            }
        }

        session.Update(
            request.SessionName,
            request.StartDate,
            request.EndDate,
            request.IsCurrent,
            request.Description,
            request.IsActive);

        await _context.SaveChangesAsync(cancellationToken);
    }
}