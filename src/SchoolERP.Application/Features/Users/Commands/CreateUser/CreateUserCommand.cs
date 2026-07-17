using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand : IRequest<int>
    {
        public string FirstName { get; init; } = string.Empty;

        public string LastName { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public string UserName { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public string? PhoneNumber { get; init; }

        public int RoleId { get; init; }
    }
}
