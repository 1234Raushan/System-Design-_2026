using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Features.AcademicSessions.DTOs;

namespace SchoolERP.Application.Features.AcademicSessions.Queries.GetAcademicSessionById;

public sealed class GetAcademicSessionByIdHandler
    : IRequestHandler<GetAcademicSessionByIdQuery, AcademicSessionDto>
{
    private readonly IApplicationDbContext _context;

    public GetAcademicSessionByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AcademicSessionDto> Handle(
        GetAcademicSessionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var session = await _context.AcademicSessions
            .AsNoTracking()
            .Where(x => x.Id == request.Id && !x.IsDeleted)
            .Select(x => new AcademicSessionDto
            {
                Id = x.Id,
                SessionName = x.SessionName,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                IsCurrent = x.IsCurrent,
                Description = x.Description,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (session is null)
            throw new KeyNotFoundException("Academic session not found.");

        return session;
    }
}