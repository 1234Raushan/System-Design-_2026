using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Features.Authentication.DTOs;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Authentication.Commands.Login;

public sealed class LoginHandler
    : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtService _jwtService;

    public LoginHandler(
        IApplicationDbContext context,
        IPasswordHasher<User> passwordHasher,
        IJwtService jwtService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var login = request.UserName.Trim();

        var user = await _context.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(
                x => x.UserName == login || x.Email == login,
                cancellationToken);

        if (user is null)
            throw new UnauthorizedAccessException("Invalid username/email or password.");

        if (!user.IsActive)
            throw new UnauthorizedAccessException("User account is inactive.");

        if (user.IsDeleted)
            throw new UnauthorizedAccessException("User account is deleted.");

        var verificationResult = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Invalid username/email or password.");

        user.UpdateLastLogin();

        await _context.SaveChangesAsync(cancellationToken);

        var token = _jwtService.GenerateToken(user);

        return new LoginResponse
        {
            UserId = user.Id,
            RoleId = user.RoleId,
            //UserName = user.UserName,
            FullName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            RoleName = user.Role.RoleName,
            AccessToken = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60) // Change later to JwtOptions
        };
    }
}