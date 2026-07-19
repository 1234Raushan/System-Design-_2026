using MediatR;

namespace SchoolERP.Application.Features.TeacherClasses.Commands.RemoveClass;

public sealed record RemoveClassCommand(int Id) : IRequest;