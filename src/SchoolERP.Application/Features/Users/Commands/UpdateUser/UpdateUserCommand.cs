using MediatR;

namespace SchoolERP.Application.Features.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand : IRequest<int>
{
    public int Id { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string? PhoneNumber { get; init; }

    public int RoleId { get; init; }
}
