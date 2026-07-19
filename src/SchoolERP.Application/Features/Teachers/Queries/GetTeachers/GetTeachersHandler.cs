using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Teachers.DTOs;

namespace SchoolERP.Application.Features.Teachers.Queries.GetTeachers;

public sealed class GetTeachersHandler
    : IRequestHandler<GetTeachersQuery, PaginatedList<TeacherDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTeachersHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<TeacherDto>> Handle(
        GetTeachersQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var query = _context.Teachers
            .AsNoTracking()
            .Where(x => !x.IsDeleted);

        // Search
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.Trim();

            query = query.Where(x =>
                x.FirstName.Contains(search) ||
                x.LastName.Contains(search) ||
                x.EmployeeCode.Contains(search) ||
                x.Email.Contains(search));
        }

        // Sorting
        query = request.SortBy?.ToLower() switch
        {
            "firstname" => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.FirstName)
                : query.OrderBy(x => x.FirstName),

            "employeecode" => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.EmployeeCode)
                : query.OrderBy(x => x.EmployeeCode),

            _ => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.CreatedDate)
                : query.OrderBy(x => x.CreatedDate)
        };

        var totalRecords = await query.CountAsync(cancellationToken);

        var teachers = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new TeacherDto
            {
                Id = x.Id,
                UserId = x.UserId,
                EmployeeCode = x.EmployeeCode,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Gender = x.Gender,
                DateOfBirth = x.DateOfBirth,
                JoiningDate = x.JoiningDate,
                Qualification = x.Qualification,
                Experience = x.Experience,
                Address = x.Address,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);

        return new PaginatedList<TeacherDto>(
            teachers,
            totalRecords,
            pageNumber,
            pageSize);
    }
}