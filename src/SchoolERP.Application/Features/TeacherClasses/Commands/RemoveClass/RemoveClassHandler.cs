using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.TeacherClasses.Commands.RemoveClass;

public sealed class RemoveClassHandler
    : IRequestHandler<RemoveClassCommand>
{
    private readonly IApplicationDbContext _context;

    public RemoveClassHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        RemoveClassCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.TeacherClasses
            .FirstOrDefaultAsync(
                x => x.Id == request.Id &&
                     !x.IsDeleted,
                cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException("Teacher class mapping not found.");

        entity.SoftDelete();

        await _context.SaveChangesAsync(cancellationToken);
    }
}