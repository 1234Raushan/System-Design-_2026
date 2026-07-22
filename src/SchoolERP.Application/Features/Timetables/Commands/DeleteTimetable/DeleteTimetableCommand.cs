using MediatR;

namespace SchoolERP.Application.Features.Timetables.Commands.DeleteTimetable;

public sealed record DeleteTimetableCommand(int Id) : IRequest<int>;