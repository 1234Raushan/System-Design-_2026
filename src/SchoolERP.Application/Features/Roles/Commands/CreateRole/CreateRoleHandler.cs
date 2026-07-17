using MediatR;
using SchoolERP.Domain.Entities;
using SchoolERP.Domain.Interfaces;

namespace SchoolERP.Application.Features.Roles.Commands.CreateRole;

public sealed class CreateRoleHandler : IRequestHandler<CreateRoleCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var roleRepository = _unitOfWork.Repository<Role>();
        var existingRoles = await roleRepository.ListAsync(
            x => !x.IsDeleted && x.RoleName.ToLower() == request.Name.Trim().ToLower(),
            cancellationToken);

        if (existingRoles.Any())
        {
            throw new InvalidOperationException("Role name already exists.");
        }

        var role = new Role(request.Name.Trim(), request.Description);
        await roleRepository.AddAsync(role, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return role.Id;
    }
}
