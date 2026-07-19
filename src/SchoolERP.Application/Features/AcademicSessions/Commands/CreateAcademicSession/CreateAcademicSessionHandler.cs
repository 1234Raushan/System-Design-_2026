using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.AcademicSessions.Commands.CreateAcademicSession;

public sealed class CreateAcademicSessionHandler
    : IRequestHandler<CreateAcademicSessionCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateAcademicSessionHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateAcademicSessionCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _context.AcademicSessions
            .AnyAsync(x =>
                x.SessionName == request.SessionName &&
                !x.IsDeleted,
                cancellationToken);

        if (exists)
            throw new InvalidOperationException("Academic session already exists.");

        // Enterprise Rule:
        // Only one session can be current.
        if (request.IsCurrent)
        {
            var currentSessions = await _context.AcademicSessions
                .Where(x => x.IsCurrent && !x.IsDeleted)
                .ToListAsync(cancellationToken);

            foreach (var session in currentSessions)
            {
                session.Update(
                    session.SessionName,
                    session.StartDate,
                    session.EndDate,
                    false,
                    session.Description,
                    session.IsActive);
            }
        }

        var entity = new AcademicSession(
            request.SessionName,
            request.StartDate,
            request.EndDate,
            request.IsCurrent,
            request.Description);

        _context.AcademicSessions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}