using AutoMapper;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Exams.DTOs;

public sealed class ExamMappingProfile : Profile
{
    public ExamMappingProfile()
    {
        CreateMap<Exam, ExamDto>()
            .ForMember(
                dest => dest.TeacherName,
                opt => opt.MapFrom(src =>
                    src.TeachingAssignment.Teacher.FirstName + " " +
                    src.TeachingAssignment.Teacher.LastName))

            .ForMember(
                dest => dest.SubjectName,
                opt => opt.MapFrom(src =>
                    src.TeachingAssignment.Subject.SubjectName))

            .ForMember(
                dest => dest.ClassName,
                opt => opt.MapFrom(src =>
                    src.TeachingAssignment.Class.ClassName))

            .ForMember(
                dest => dest.SectionName,
                opt => opt.MapFrom(src =>
                    src.TeachingAssignment.Section.SectionName));
    }
}