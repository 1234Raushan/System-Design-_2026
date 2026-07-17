using FluentValidation;

namespace SchoolERP.Application.Features.Users.Commands.DeleteUser;

public sealed class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
