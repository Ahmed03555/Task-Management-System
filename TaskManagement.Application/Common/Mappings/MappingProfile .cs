using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Model.Projects.Common;
using TaskManagement.Application.Model.Tasks.Commands.Queries.GetTasksByProject;
using TaskManagement.Application.Model.Users.Commands.Queries.GetAllUsers;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Common.Mappings
{
    #region Mapping
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Task
            CreateMap<TaskItem, TaskDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Satus.ToString()))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()));
            #endregion

            #region Project

            CreateMap<Project, ProjectDto>();

            #endregion

            #region User
            CreateMap<User, UserDto>()
                .ForMember(d => d.Role, opt => opt.MapFrom(s => s.Role.ToString()));
            #endregion
        }
    } 
    #endregion
}
