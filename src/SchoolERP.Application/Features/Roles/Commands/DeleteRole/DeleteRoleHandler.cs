using MediatR;
using SchoolERP.Domain.Entities;
using SchoolERP.Domain.Interfaces;

namespace SchoolERP.Application.Features.Roles.Commands.DeleteRole;

public sealed class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var roleRepository = _unitOfWork.Repository<Role>();
        var role = (await roleRepository.ListAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken)).FirstOrDefault();

        if (role is null)
        {
            throw new InvalidOperationException("Role not found.");
        }

        role.SoftDelete();
        role.SetUpdatedDate();
        role.SetUpdatedBy(0);

        await roleRepository.UpdateAsync(role, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return role.Id;
    }
}
