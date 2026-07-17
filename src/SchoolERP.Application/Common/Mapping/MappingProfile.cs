using AutoMapper;
using SchoolERP.Application.Features.Roles.Commands.CreateRole;
using SchoolERP.Application.Features.Roles.Commands.UpdateRole;
using SchoolERP.Application.Features.Roles.DTOs;
using SchoolERP.Application.Features.Users.Commands.CreateUser;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Common.Mapping
{
    internal sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName));

            CreateMap<CreateRoleRequest, CreateRoleCommand>();
            CreateMap<UpdateRoleRequest, UpdateRoleCommand>();
        }
    }
}
