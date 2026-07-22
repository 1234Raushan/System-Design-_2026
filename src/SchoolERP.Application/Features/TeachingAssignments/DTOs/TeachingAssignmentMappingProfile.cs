using AutoMapper;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.TeachingAssignments.DTOs;

public sealed class TeachingAssignmentMappingProfile
    : Profile
{
    public TeachingAssignmentMappingProfile()
    {
        CreateMap<TeachingAssignment, TeachingAssignmentDto>()
            .ForMember(
                dest => dest.TeacherName,
                opt => opt.MapFrom(
                    src => src.Teacher.FirstName
                         + " "
                         + src.Teacher.LastName))

            .ForMember(
                dest => dest.SubjectName,
                opt => opt.MapFrom(
                    src => src.Subject.SubjectName))

            .ForMember(
                dest => dest.ClassName,
                opt => opt.MapFrom(
                    src => src.Class.ClassName))

            .ForMember(
                dest => dest.SectionName,
                opt => opt.MapFrom(
                    src => src.Section.SectionName));
    }
}