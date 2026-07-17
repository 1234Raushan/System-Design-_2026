using MediatR;
using SchoolERP.Application.Features.Students.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.Students.Queries.GetStudentById
{
    public sealed record GetStudentByIdQuery(int Id)
        : IRequest<StudentDto>;
}
