using MediatR;

namespace SchoolERP.Application.Features.Marks.Queries.GetMarkById;

public sealed record GetMarkByIdQuery(
    int Id
) : IRequest<MarkListDto>;