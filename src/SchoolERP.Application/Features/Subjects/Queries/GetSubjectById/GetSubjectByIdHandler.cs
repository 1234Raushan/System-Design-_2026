using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Features.Subjects.DTOs;

namespace SchoolERP.Application.Features.Subjects.Queries.GetSubjectById;

public sealed class GetSubjectByIdHandler
    : IRequestHandler<GetSubjectByIdQuery, SubjectDto>
{
    private readonly IApplicationDbContext _context;

    public GetSubjectByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SubjectDto> Handle(
        GetSubjectByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.Subjects
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == request.Id &&
                     !x.IsDeleted,
                cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException("Subject not found.");

        return new SubjectDto
        {
            Id = entity.Id,
            SubjectName = entity.SubjectName,
            SubjectCode = entity.SubjectCode,
            Description = entity.Description,
            IsActive = entity.IsActive
        };
    }
}