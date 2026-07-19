using MediatR;

namespace SchoolERP.Application.Features.Teachers.Commands.CreateTeacher;

public sealed record CreateTeacherCommand : IRequest<int>
{
    public int UserId { get; init; }

    public string EmployeeCode { get; init; } = string.Empty;

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string? PhoneNumber { get; init; }

    public string? Gender { get; init; }

    public DateTime? DateOfBirth { get; init; }

    public DateTime JoiningDate { get; init; }

    public string? Qualification { get; init; }

    public int? Experience { get; init; }

    public string? Address { get; init; }
}