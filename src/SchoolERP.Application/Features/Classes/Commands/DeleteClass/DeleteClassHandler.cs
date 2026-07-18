using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Classes.Commands.DeleteClass;

public sealed class DeleteClassHandler
    : IRequestHandler<DeleteClassCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteClassHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteClassCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.classes
            .FirstOrDefaultAsync(
                x => x.Id == request.Id && !x.IsDeleted,
                cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException("Class not found.");

        entity.SoftDelete();

        await _context.SaveChangesAsync(cancellationToken);
    }
}