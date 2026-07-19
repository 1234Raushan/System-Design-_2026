using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.TeacherClasses.Commands.AssignClass;

public sealed class AssignClassHandler
    : IRequestHandler<AssignClassCommand, int>
{
    private readonly IApplicationDbContext _context;

    public AssignClassHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        AssignClassCommand request,
        CancellationToken cancellationToken)
    {
        var teacherExists = await _context.Teachers
            .AnyAsync(x => x.Id == request.TeacherId && !x.IsDeleted, cancellationToken);

        if (!teacherExists)
            throw new KeyNotFoundException("Teacher not found.");

        var classExists = await _context.classes
            .AnyAsync(x => x.Id == request.ClassId && !x.IsDeleted, cancellationToken);

        if (!classExists)
            throw new KeyNotFoundException("Class not found.");

        var alreadyAssigned = await _context.TeacherClasses
            .AnyAsync(x =>
                x.TeacherId == request.TeacherId &&
                x.ClassId == request.ClassId &&
                !x.IsDeleted,
                cancellationToken);

        if (alreadyAssigned)
            throw new InvalidOperationException("Class already assigned to this teacher.");

        var entity = new TeacherClass(
            request.TeacherId,
            request.ClassId);

        _context.TeacherClasses.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}