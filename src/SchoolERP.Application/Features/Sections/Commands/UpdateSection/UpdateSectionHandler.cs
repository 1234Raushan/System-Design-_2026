using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Sections.Commands.UpdateSection;

public sealed class UpdateSectionHandler
    : IRequestHandler<UpdateSectionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSectionHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateSectionCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.Sections
            .FirstOrDefaultAsync(
                x => x.Id == request.Id && !x.IsDeleted,
                cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException("Section not found.");

        var classExists = await _context.classes
            .AnyAsync(
                x => x.Id == request.ClassId && !x.IsDeleted,
                cancellationToken);

        if (!classExists)
            throw new InvalidOperationException("Class not found.");

        var duplicate = await _context.Sections.AnyAsync(
            x => x.Id != request.Id &&
                 x.ClassId == request.ClassId &&
                 x.SectionName == request.SectionName &&
                 !x.IsDeleted,
            cancellationToken);

        if (duplicate)
            throw new InvalidOperationException(
                "Section already exists for this class.");

        entity.Update(
            request.SectionName,
            request.ClassId,
            request.IsActive);

        await _context.SaveChangesAsync(cancellationToken);
    }
}