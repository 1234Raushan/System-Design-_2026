using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Students.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.Students.Queries.GetStudents
{
    public sealed class GetStudentsHandler
        : IRequestHandler<GetStudentsQuery, PaginatedList<StudentDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentsHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<StudentDto>> Handle(
            GetStudentsQuery request,
            CancellationToken cancellationToken)
        {
            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var query = _context.Students
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var search = request.SearchTerm.Trim();

                query = query.Where(x =>
                    x.FirstName.Contains(search) ||
                    x.LastName.Contains(search) ||
                    x.AdmissionNumber.Contains(search)
                    );
            }

            var totalRecords = await query.CountAsync(cancellationToken);

            // Sorting
            query = request.SortBy?.ToLower() switch
            {
                "firstname" => request.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.FirstName)
                    : query.OrderBy(x => x.FirstName),

                "lastname" => request.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.LastName)
                    : query.OrderBy(x => x.LastName),

                "admissionnumber" => request.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.AdmissionNumber)
                    : query.OrderBy(x => x.AdmissionNumber),

                _ => request.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.CreatedDate)
                    : query.OrderBy(x => x.CreatedDate)
            };

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new StudentDto
                {
                    Id = x.Id,
                    AdmissionNumber = x.AdmissionNumber,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DateOfBirth = x.DateOfBirth,
                    Gender = x.Gender,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Address = x.Address,
                    IsActive = x.IsActive
                })
                .ToListAsync(cancellationToken);

            return new PaginatedList<StudentDto>(
                items,
                totalRecords,
                pageNumber,
                pageSize);
        }
    }
}
