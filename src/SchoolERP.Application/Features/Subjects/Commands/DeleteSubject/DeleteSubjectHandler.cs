using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Subjects.Commands.DeleteSubject;

public sealed class DeleteSubjectHandler
    : IRequestHandler<DeleteSubjectCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteSubjectHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteSubjectCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.Subjects
            .FirstOrDefaultAsync(
                x => x.Id == request.Id &&
                     !x.IsDeleted,
                cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException("Subject not found.");

        entity.SoftDelete();

        await _context.SaveChangesAsync(cancellationToken);
    }
}