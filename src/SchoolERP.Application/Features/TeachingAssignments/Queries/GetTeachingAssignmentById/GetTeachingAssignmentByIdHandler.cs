using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Features.TeachingAssignments.DTOs;

namespace SchoolERP.Application.Features.TeachingAssignments.Queries.GetTeachingAssignmentById;

public sealed class GetTeachingAssignmentByIdHandler
    : IRequestHandler<GetTeachingAssignmentByIdQuery, TeachingAssignmentDto>
{
    private readonly IApplicationDbContext _context;

    public GetTeachingAssignmentByIdHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TeachingAssignmentDto> Handle(
        GetTeachingAssignmentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var teachingAssignment = await _context.TeachingAssignments
            .AsNoTracking()
            .Include(x => x.Teacher)
            .Include(x => x.Subject)
            .Include(x => x.Class)
            .Include(x => x.Section)
            .Where(x =>
                x.Id == request.Id &&
                !x.IsDeleted)
            .Select(x => new TeachingAssignmentDto
            {
                Id = x.Id,

                TeacherId = x.TeacherId,
                TeacherName =
                    x.Teacher.FirstName + " " + x.Teacher.LastName,

                SubjectId = x.SubjectId,
                SubjectName = x.Subject.SubjectName,

                ClassId = x.ClassId,
                ClassName = x.Class.ClassName,

                SectionId = x.SectionId,
                SectionName = x.Section.SectionName,

                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);


        if (teachingAssignment is null)
            throw new InvalidOperationException(
                "Teaching Assignment not found.");


        return teachingAssignment;
    }
}