using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Subjects.Commands.UpdateSubject;

public sealed class UpdateSubjectHandler
    : IRequestHandler<UpdateSubjectCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSubjectHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateSubjectCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.Subjects
            .FirstOrDefaultAsync(
                x => x.Id == request.Id &&
                     !x.IsDeleted,
                cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException("Subject not found.");

        var duplicate = await _context.Subjects.AnyAsync(
            x => x.Id != request.Id &&
                 x.SubjectCode == request.SubjectCode &&
                 !x.IsDeleted,
            cancellationToken);

        if (duplicate)
            throw new InvalidOperationException("Subject code already exists.");

        entity.Update(
            request.SubjectName,
            request.SubjectCode,
            request.Description,
            request.IsActive);

        await _context.SaveChangesAsync(cancellationToken);
    }
}