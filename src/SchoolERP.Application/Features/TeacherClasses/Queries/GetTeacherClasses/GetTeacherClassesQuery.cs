using MediatR;

namespace SchoolERP.Application.Features.TeacherClasses.Queries.GetTeacherClasses;

public sealed record GetTeacherClassesQuery(int TeacherId)
    : IRequest<List<TeacherClassDto>>;