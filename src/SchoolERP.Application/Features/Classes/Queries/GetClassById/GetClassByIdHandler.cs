using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Features.Classes.DTOs;

namespace SchoolERP.Application.Features.Classes.Queries.GetClassById;

public sealed class GetClassByIdHandler
    : IRequestHandler<GetClassByIdQuery, ClassDto>
{
    private readonly IApplicationDbContext _context;

    public GetClassByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ClassDto> Handle(
        GetClassByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.classes
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == request.Id && !x.IsDeleted,
                cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException("Class not found.");

        return new ClassDto
        {
            Id = entity.Id,
            ClassName = entity.ClassName,
            ClassCode = entity.ClassCode,
            Description = entity.Description,
            IsActive = entity.IsActive
        };
    }
}