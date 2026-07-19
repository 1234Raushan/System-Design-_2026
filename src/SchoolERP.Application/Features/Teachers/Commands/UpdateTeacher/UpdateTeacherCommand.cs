using MediatR;

namespace SchoolERP.Application.Features.Teachers.Commands.UpdateTeacher;

public sealed record UpdateTeacherCommand : IRequest
{
    public int Id { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string? PhoneNumber { get; init; }

    public string? Gender { get; init; }

    public DateTime? DateOfBirth { get; init; }

    public string? Qualification { get; init; }

    public int? Experience { get; init; }

    public string? Address { get; init; }

    public bool IsActive { get; init; }
}