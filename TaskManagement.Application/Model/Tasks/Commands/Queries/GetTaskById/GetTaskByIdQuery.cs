using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Model.Tasks.Commands.Queries.GetTasksByProject;

namespace TaskManagement.Application.Model.Tasks.Commands.Queries.GetTaskById
{
    public record GetTaskByIdQuery(Guid Id) : IRequest<Result<TaskDto>>;
}
