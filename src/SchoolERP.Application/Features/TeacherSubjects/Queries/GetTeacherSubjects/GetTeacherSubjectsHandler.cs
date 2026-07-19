using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Features.TeacherSubjects.DTOs.GetTeacherSubjects;

namespace SchoolERP.Application.Features.TeacherSubjects.Queries.GetTeacherSubjects;

public sealed class GetTeacherSubjectsHandler
    : IRequestHandler<GetTeacherSubjectsQuery, List<TeacherSubjectDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTeacherSubjectsHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TeacherSubjectDto>> Handle(
        GetTeacherSubjectsQuery request,
        CancellationToken cancellationToken)
    {
        var teacherExists = await _context.Teachers
            .AnyAsync(x => x.Id == request.TeacherId && !x.IsDeleted,
                cancellationToken);

        if (!teacherExists)
            throw new KeyNotFoundException("Teacher not found.");

        return await _context.TeacherSubjects
            .AsNoTracking()
            .Where(x =>
                x.TeacherId == request.TeacherId &&
                !x.IsDeleted)
            .Select(x => new TeacherSubjectDto
            {
                Id = x.Id,
                SubjectId = x.SubjectId,
                SubjectName = x.Subject.SubjectName,
                SubjectCode = x.Subject.SubjectCode
            })
            .OrderBy(x => x.SubjectName)
            .ToListAsync(cancellationToken);
    }
}