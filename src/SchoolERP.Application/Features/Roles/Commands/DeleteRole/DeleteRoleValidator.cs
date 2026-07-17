using FluentValidation;

namespace SchoolERP.Application.Features.Roles.Commands.DeleteRole;

public sealed class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
