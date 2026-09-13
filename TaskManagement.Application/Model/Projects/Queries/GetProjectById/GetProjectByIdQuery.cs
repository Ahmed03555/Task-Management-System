using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Model.Projects.Common;

namespace TaskManagement.Application.Model.Projects.Queries.GetProjectById
{
    public record GetProjectByIdQuery(Guid Id) : IRequest<Result<ProjectDto>>;
}
