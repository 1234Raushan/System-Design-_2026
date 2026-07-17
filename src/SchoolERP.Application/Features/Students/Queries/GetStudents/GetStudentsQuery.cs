using MediatR;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Students.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.Students.Queries.GetStudents
{
    public sealed record GetStudentsQuery
        : PagedQuery,
          IRequest<PaginatedList<StudentDto>>;
}
