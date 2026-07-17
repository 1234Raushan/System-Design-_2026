using MediatR;
using SchoolERP.Application.Features.Authentication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.Authentication.Commands.Login
{
    public sealed record LoginCommand(
        string UserName,
        string Password
    ) : IRequest<LoginResponse>;
}
