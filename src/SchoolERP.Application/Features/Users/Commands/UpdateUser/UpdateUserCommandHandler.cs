using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken);

        if (user is null)
        {
            throw new InvalidOperationException("User not found.");
        }

        if (await _context.Users.AnyAsync(x => x.Id != request.Id && x.Email == request.Email, cancellationToken))
        {
            throw new InvalidOperationException("Email already exists.");
        }

        if (await _context.Users.AnyAsync(x => x.Id != request.Id && x.UserName == user.UserName, cancellationToken))
        {
            throw new InvalidOperationException("Username already exists.");
        }

        var roleExists = await _context.Roles.AnyAsync(x => x.Id == request.RoleId, cancellationToken);
        if (!roleExists)
        {
            throw new InvalidOperationException("Invalid role.");
        }

        user.UpdateName(request.FirstName, request.LastName);
        user.SetEmail(request.Email);
        user.AssignRole(request.RoleId);
        user.UpdatePhoneNumber(request.PhoneNumber);
        user.SetUpdatedDate();

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
