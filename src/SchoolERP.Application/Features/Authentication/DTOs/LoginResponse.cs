using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.Authentication.DTOs
{
public sealed class LoginResponse
{
    public int UserId { get; init; }

    //public string UserName { get; init; } = string.Empty;

    public string FullName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public int RoleId { get; init; }

    public string RoleName { get; init; } = string.Empty;

    public string AccessToken { get; init; } = string.Empty;

    public DateTime ExpiresAt { get; init; }
}
}
