using AutoMapper;
using SchoolERP.Application.Features.Timetables.Commands.CreateTimetable;
using SchoolERP.Application.Features.Timetables.Commands.UpdateTimetable;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Timetables.DTOs;

public sealed class TimetableMappingProfile : Profile
{
    public TimetableMappingProfile()
    {
        CreateMap<CreateTimetableRequest, CreateTimetableCommand>();

        CreateMap<UpdateTimetableRequest, UpdateTimetableCommand>();

        CreateMap<Timetable, TimetableDto>();
    }
}