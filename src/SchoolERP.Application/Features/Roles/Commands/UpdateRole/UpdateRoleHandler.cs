using MediatR;
using SchoolERP.Domain.Entities;
using SchoolERP.Domain.Interfaces;

namespace SchoolERP.Application.Features.Roles.Commands.UpdateRole;

public sealed class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var roleRepository = _unitOfWork.Repository<Role>();
        var role = (await roleRepository.ListAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken)).FirstOrDefault();

        if (role is null)
        {
            throw new InvalidOperationException("Role not found.");
        }

        var duplicateRoles = await roleRepository.ListAsync(
            x => x.Id != request.Id && !x.IsDeleted && x.RoleName.ToLower() == request.Name.Trim().ToLower(),
            cancellationToken);

        if (duplicateRoles.Any())
        {
            throw new InvalidOperationException("Role name already exists.");
        }

        role.SetName(request.Name.Trim());
        role.UpdateDescription(request.Description);

        if (request.IsActive)
        {
            role.Activate();
        }
        else
        {
            role.Deactivate();
        }

        role.SetUpdatedDate();
        role.SetUpdatedBy(0);

        await roleRepository.UpdateAsync(role, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return role.Id;
    }
}
