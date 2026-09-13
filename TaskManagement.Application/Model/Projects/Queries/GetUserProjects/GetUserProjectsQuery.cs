using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Model.Projects.Common;

namespace TaskManagement.Application.Model.Projects.Queries.GetUserProjects
{
    public record GetUserProjectsQuery() : IRequest<Result<List<ProjectDto>>>;
    
    
}
