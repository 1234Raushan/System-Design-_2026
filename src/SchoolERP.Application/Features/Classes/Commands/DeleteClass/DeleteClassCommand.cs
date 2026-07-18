using MediatR;

namespace SchoolERP.Application.Features.Classes.Commands.DeleteClass;

public sealed record DeleteClassCommand(int Id) : IRequest;