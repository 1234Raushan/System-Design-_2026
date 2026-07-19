using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Sections.Commands.DeleteSection;

public sealed class DeleteSectionHandler
    : IRequestHandler<DeleteSectionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteSectionHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteSectionCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.Sections
            .FirstOrDefaultAsync(
                x => x.Id == request.Id &&
                     !x.IsDeleted,
                cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException("Section not found.");

        entity.SoftDelete();

        await _context.SaveChangesAsync(cancellationToken);
    }
}