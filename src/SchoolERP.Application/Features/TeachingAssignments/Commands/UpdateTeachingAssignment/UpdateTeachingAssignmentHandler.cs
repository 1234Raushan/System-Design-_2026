using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.TeachingAssignments.Commands.UpdateTeachingAssignment;

public sealed class UpdateTeachingAssignmentHandler
    : IRequestHandler<UpdateTeachingAssignmentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTeachingAssignmentHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateTeachingAssignmentCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Teaching Assignment should exist
        var teachingAssignment = await _context.TeachingAssignments
            .FirstOrDefaultAsync(x =>
                x.Id == request.Id &&
                !x.IsDeleted,
                cancellationToken);

        if (teachingAssignment is null)
            throw new InvalidOperationException(
                "Teaching Assignment not found.");

        // 2. Teacher should exist
        var teacherExists = await _context.Teachers
            .AnyAsync(x =>
                x.Id == request.TeacherId &&
                !x.IsDeleted,
                cancellationToken);

        if (!teacherExists)
            throw new InvalidOperationException(
                "Teacher not found.");

        // 3. Subject should exist
        var subjectExists = await _context.Subjects
            .AnyAsync(x =>
                x.Id == request.SubjectId &&
                !x.IsDeleted,
                cancellationToken);

        if (!subjectExists)
            throw new InvalidOperationException(
                "Subject not found.");

        // 4. Class should exist
        var classExists = await _context.classes
            .AnyAsync(x =>
                x.Id == request.ClassId &&
                !x.IsDeleted,
                cancellationToken);

        if (!classExists)
            throw new InvalidOperationException(
                "Class not found.");

        // 5. Section should exist and belong to selected class
        var sectionExists = await _context.Sections
            .AnyAsync(x =>
                x.Id == request.SectionId &&
                x.ClassId == request.ClassId &&
                !x.IsDeleted,
                cancellationToken);

        if (!sectionExists)
            throw new InvalidOperationException(
                "Section not found or does not belong to selected class.");

        // 6. Duplicate Teaching Assignment check
        var duplicateExists = await _context.TeachingAssignments
            .AnyAsync(x =>
                x.Id != request.Id &&
                x.TeacherId == request.TeacherId &&
                x.SubjectId == request.SubjectId &&
                x.ClassId == request.ClassId &&
                x.SectionId == request.SectionId &&
                !x.IsDeleted,
                cancellationToken);

        if (duplicateExists)
            throw new InvalidOperationException(
                "Teaching Assignment already exists.");

        // 7. Update Entity
        teachingAssignment.Update(
            request.TeacherId,
            request.SubjectId,
            request.ClassId,
            request.SectionId,
            request.IsActive);

        // 8. Save Changes
        await _context.SaveChangesAsync(cancellationToken);
    }
}