using MediatR;
using SchoolERP.Application.Features.Sections.DTOs;

namespace SchoolERP.Application.Features.Sections.Queries.GetSectionById;

public sealed record GetSectionByIdQuery(int Id)
    : IRequest<SectionDto>;