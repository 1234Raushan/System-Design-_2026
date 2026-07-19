using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Features.Sections.DTOs;

namespace SchoolERP.Application.Features.Sections.Queries.GetSectionById;

public sealed class GetSectionByIdHandler
    : IRequestHandler<GetSectionByIdQuery, SectionDto>
{
    private readonly IApplicationDbContext _context;

    public GetSectionByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SectionDto> Handle(
        GetSectionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.Sections
            .AsNoTracking()
            .Include(x => x.Class)
            .FirstOrDefaultAsync(
                x => x.Id == request.Id && !x.IsDeleted,
                cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException("Section not found.");

        return new SectionDto
        {
            Id = entity.Id,
            SectionName = entity.SectionName,
            ClassId = entity.ClassId,
            ClassName = entity.Class.ClassName,
            IsActive = entity.IsActive
        };
    }
}