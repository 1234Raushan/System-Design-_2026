using MediatR;
using SchoolERP.Application.Features.Timetables.DTOs;

namespace SchoolERP.Application.Features.Timetables.Queries.GetTimetableById;

public sealed record GetTimetableByIdQuery(int Id) : IRequest<TimetableDto?>;