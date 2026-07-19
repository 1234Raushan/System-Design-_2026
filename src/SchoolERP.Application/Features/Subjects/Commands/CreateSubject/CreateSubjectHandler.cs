using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Subjects.Commands.CreateSubject;

public sealed class CreateSubjectHandler
    : IRequestHandler<CreateSubjectCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateSubjectHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateSubjectCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _context.Subjects.AnyAsync(
            x => x.SubjectCode == request.SubjectCode &&
                 !x.IsDeleted,
            cancellationToken);

        if (exists)
            throw new InvalidOperationException("Subject code already exists.");

        var entity = new Subject(
            request.SubjectName,
            request.SubjectCode,
            request.Description,
            request.IsActive);

        _context.Subjects.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}