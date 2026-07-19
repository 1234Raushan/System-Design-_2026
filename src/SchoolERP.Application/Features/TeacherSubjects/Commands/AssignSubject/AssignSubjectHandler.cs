using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.TeacherSubjects.Commands.AssignSubject;

public sealed class AssignSubjectHandler
    : IRequestHandler<AssignSubjectCommand, int>
{
    private readonly IApplicationDbContext _context;

    public AssignSubjectHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        AssignSubjectCommand request,
        CancellationToken cancellationToken)
    {
        // Teacher exists?
        var teacherExists = await _context.Teachers
            .AnyAsync(x => x.Id == request.TeacherId && !x.IsDeleted,
                cancellationToken);

        if (!teacherExists)
            throw new KeyNotFoundException("Teacher not found.");

        // Subject exists?
        var subjectExists = await _context.Subjects
            .AnyAsync(x => x.Id == request.SubjectId && !x.IsDeleted,
                cancellationToken);

        if (!subjectExists)
            throw new KeyNotFoundException("Subject not found.");

        // Already assigned?
        var alreadyAssigned = await _context.TeacherSubjects
            .AnyAsync(x =>
                x.TeacherId == request.TeacherId &&
                x.SubjectId == request.SubjectId &&
                !x.IsDeleted,
                cancellationToken);

        if (alreadyAssigned)
            throw new InvalidOperationException(
                "Subject is already assigned to this teacher.");

        var entity = new TeacherSubject(
            request.TeacherId,
            request.SubjectId);

        _context.TeacherSubjects.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}