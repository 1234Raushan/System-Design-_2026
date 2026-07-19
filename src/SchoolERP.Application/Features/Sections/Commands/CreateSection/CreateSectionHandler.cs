using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Sections.Commands.CreateSection;

public sealed class CreateSectionHandler
    : IRequestHandler<CreateSectionCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateSectionHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateSectionCommand request,
        CancellationToken cancellationToken)
    {
        var classExists = await _context.classes
            .AnyAsync(x => x.Id == request.ClassId && !x.IsDeleted,
                cancellationToken);

        if (!classExists)
            throw new InvalidOperationException("Class not found.");

        var exists = await _context.Sections.AnyAsync(
            x => x.ClassId == request.ClassId &&
                 x.SectionName == request.SectionName &&
                 !x.IsDeleted,
            cancellationToken);

        if (exists)
            throw new InvalidOperationException(
                "Section already exists for this class.");

        var entity = new Section(
            request.SectionName,
            request.ClassId);

        _context.Sections.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}