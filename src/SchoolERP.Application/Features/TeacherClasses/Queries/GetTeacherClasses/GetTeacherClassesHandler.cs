using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.TeacherClasses.Queries.GetTeacherClasses;

public sealed class GetTeacherClassesHandler
    : IRequestHandler<GetTeacherClassesQuery, List<TeacherClassDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTeacherClassesHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TeacherClassDto>> Handle(
        GetTeacherClassesQuery request,
        CancellationToken cancellationToken)
    {
        var teacherExists = await _context.Teachers
            .AnyAsync(x => x.Id == request.TeacherId && !x.IsDeleted,
                cancellationToken);

        if (!teacherExists)
            throw new KeyNotFoundException("Teacher not found.");

        return await _context.TeacherClasses
            .AsNoTracking()
            .Where(x => x.TeacherId == request.TeacherId &&
                        !x.IsDeleted)
            .Select(x => new TeacherClassDto
            {
                Id = x.Id,
                ClassId = x.ClassId,
                ClassName = x.Class.ClassName,
                ClassCode = x.Class.ClassCode
            })
            .OrderBy(x => x.ClassName)
            .ToListAsync(cancellationToken);
    }
}