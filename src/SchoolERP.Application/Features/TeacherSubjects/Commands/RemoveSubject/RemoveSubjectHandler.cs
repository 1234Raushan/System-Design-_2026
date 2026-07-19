using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.TeacherSubjects.Commands.RemoveSubject;

public sealed class RemoveSubjectHandler
    : IRequestHandler<RemoveSubjectCommand>
{
    private readonly IApplicationDbContext _context;

    public RemoveSubjectHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        RemoveSubjectCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.TeacherSubjects
            .FirstOrDefaultAsync(
                x => x.Id == request.Id &&
                     !x.IsDeleted,
                cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException("Teacher subject mapping not found.");

        entity.SoftDelete();

        await _context.SaveChangesAsync(cancellationToken);
    }
}