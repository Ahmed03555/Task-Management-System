using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Model.Tasks.Commands.Queries.GetTasksByProject;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Common.Mappings
{
    #region Mapping
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskItem, TaskDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Satus.ToString()))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()));
        }
    } 
    #endregion
}
