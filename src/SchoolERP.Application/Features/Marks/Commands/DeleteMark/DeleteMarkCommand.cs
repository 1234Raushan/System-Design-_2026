using MediatR;

namespace SchoolERP.Application.Features.Marks.Commands.DeleteMark;

public sealed record DeleteMarkCommand(
    int Id
) : IRequest;