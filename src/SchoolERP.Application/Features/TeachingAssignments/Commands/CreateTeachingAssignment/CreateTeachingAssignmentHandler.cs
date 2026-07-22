using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.TeachingAssignments.Commands.CreateTeachingAssignment;

public sealed class CreateTeachingAssignmentHandler
    : IRequestHandler<CreateTeachingAssignmentCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTeachingAssignmentHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateTeachingAssignmentCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Teacher should exist
        var teacherExists = await _context.Teachers
            .AnyAsync(x => x.Id == request.TeacherId &&
                           !x.IsDeleted,
                cancellationToken);

        if (!teacherExists)
            throw new InvalidOperationException("Teacher not found.");

        // 2. Subject should exist
        var subjectExists = await _context.Subjects
            .AnyAsync(x => x.Id == request.SubjectId &&
                           !x.IsDeleted,
                cancellationToken);

        if (!subjectExists)
            throw new InvalidOperationException("Subject not found.");

        // 3. Class should exist
        var classExists = await _context.classes
            .AnyAsync(x => x.Id == request.ClassId &&
                           !x.IsDeleted,
                cancellationToken);

        if (!classExists)
            throw new InvalidOperationException("Class not found.");

        // 4. Section should exist
        var sectionExists = await _context.Sections
            .AnyAsync(x => x.Id == request.SectionId &&
                           !x.IsDeleted,
                cancellationToken);

        if (!sectionExists)
            throw new InvalidOperationException("Section not found.");

        // 5. Duplicate Teaching Assignment should not exist
        var assignmentExists = await _context.TeachingAssignments
            .AnyAsync(x =>
                x.TeacherId == request.TeacherId &&
                x.SubjectId == request.SubjectId &&
                x.ClassId == request.ClassId &&
                x.SectionId == request.SectionId &&
                !x.IsDeleted,
                cancellationToken);

        if (assignmentExists)
            throw new InvalidOperationException(
                "Teaching Assignment already exists.");

        // 6. Create Teaching Assignment
        var teachingAssignment = new TeachingAssignment(
            request.TeacherId,
            request.SubjectId,
            request.ClassId,
            request.SectionId);

        _context.TeachingAssignments.Add(teachingAssignment);

        // 7. Save Changes
        await _context.SaveChangesAsync(cancellationToken);

        return teachingAssignment.Id;
    }
}