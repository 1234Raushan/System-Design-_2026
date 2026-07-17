using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.Users.Commands.CreateUser
{
    public sealed class CreateUserHandler
        : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public CreateUserHandler(
            IApplicationDbContext context,
            IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<int> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            if (await _context.Users.AnyAsync(x => x.Email == request.Email, cancellationToken))
                throw new InvalidOperationException("Email already exists.");

            if (await _context.Users.AnyAsync(x => x.UserName == request.UserName, cancellationToken))
                throw new InvalidOperationException("Username already exists.");

            if (request.RoleId <= 0)
                throw new InvalidOperationException("Role is required.");

            var roleExists = await _context.Roles
                .AnyAsync(x => x.Id == request.RoleId, cancellationToken);

            if (!roleExists)
                throw new InvalidOperationException("Invalid role.");

            var user = new User(
                request.FirstName,
                request.LastName,
                request.Email,
                request.UserName,
                string.Empty,
                request.RoleId,
                request.PhoneNumber);

            var passwordHash = _passwordHasher.HashPassword(user, request.Password);
            user.ChangePassword(passwordHash);

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);         
            return user.Id;
        }
    }
}
