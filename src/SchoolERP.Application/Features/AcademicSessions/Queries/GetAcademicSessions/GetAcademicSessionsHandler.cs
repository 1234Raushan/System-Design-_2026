using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Features.AcademicSessions.DTOs;

namespace SchoolERP.Application.Features.AcademicSessions.Queries.GetAcademicSessions;

public sealed class GetAcademicSessionsHandler
    : IRequestHandler<GetAcademicSessionsQuery, List<AcademicSessionDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAcademicSessionsHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AcademicSessionDto>> Handle(GetAcademicSessionsQuery request,CancellationToken cancellationToken)
    {
        return await _context.AcademicSessions
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.IsCurrent)
            .ThenByDescending(x => x.StartDate)
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
            .ToListAsync(cancellationToken);
    }
}