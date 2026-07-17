using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Features.Roles.DTOs;

namespace SchoolERP.Application.Features.Roles.Queries.GetRoleById;

public sealed class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly IApplicationDbContext _context;

    public GetRoleByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken);

        if (role is null)
        {
            return null;
        }

        return new RoleDto
        {
            Id = role.Id,
            Name = role.RoleName,
            Description = role.Description,
            IsActive = role.IsActive,
            IsDeleted = role.IsDeleted
        };
    }
}
