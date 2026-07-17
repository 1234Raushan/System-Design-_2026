using MediatR;
using SchoolERP.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.Users.Queries.GetUserById
{
    public sealed record GetUserByIdQuery(int Id)
        : IRequest<UserDto>;
}
