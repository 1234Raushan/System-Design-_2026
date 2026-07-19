using MediatR;
using SchoolERP.Application.Features.TeacherSubjects.DTOs.GetTeacherSubjects;

namespace SchoolERP.Application.Features.TeacherSubjects.Queries.GetTeacherSubjects;

public sealed record GetTeacherSubjectsQuery(int TeacherId)
    : IRequest<List<TeacherSubjectDto>>;