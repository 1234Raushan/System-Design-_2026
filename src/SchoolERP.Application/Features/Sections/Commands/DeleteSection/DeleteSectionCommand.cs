using MediatR;

namespace SchoolERP.Application.Features.Sections.Commands.DeleteSection;

public sealed record DeleteSectionCommand(int Id) : IRequest;