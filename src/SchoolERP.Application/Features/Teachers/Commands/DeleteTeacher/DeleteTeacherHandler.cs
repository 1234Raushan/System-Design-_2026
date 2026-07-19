using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Teachers.Commands.DeleteTeacher;

public sealed class DeleteTeacherHandler
    : IRequestHandler<DeleteTeacherCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTeacherHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteTeacherCommand request,
        CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(
                x => x.Id == request.Id &&
                     !x.IsDeleted,
                cancellationToken);

        if (teacher is null)
            throw new KeyNotFoundException("Teacher not found.");

        teacher.SoftDelete();

        await _context.SaveChangesAsync(cancellationToken);
    }
}