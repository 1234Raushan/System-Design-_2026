using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Classes.Commands.UpdateClass;

public sealed class UpdateClassHandler
    : IRequestHandler<UpdateClassCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateClassHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateClassCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.classes
            .FirstOrDefaultAsync(
                x => x.Id == request.Id && !x.IsDeleted,
                cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException("Class not found.");

        if (await _context.classes.AnyAsync(
            x => x.Id != request.Id &&
                 x.ClassName == request.ClassName,
            cancellationToken))
        {
            throw new InvalidOperationException("Class name already exists.");
        }

        if (await _context.classes.AnyAsync(
            x => x.Id != request.Id &&
                 x.ClassCode == request.ClassCode,
            cancellationToken))
        {
            throw new InvalidOperationException("Class code already exists.");
        }

        entity.Update(
            request.ClassName,
            request.ClassCode,
            request.Description,
            request.IsActive);

        await _context.SaveChangesAsync(cancellationToken);
    }
}