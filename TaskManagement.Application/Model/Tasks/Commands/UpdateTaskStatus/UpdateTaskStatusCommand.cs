using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Model.Tasks.Commands.UpdateTaskStatus
{
    public record UpdateTaskStatusCommand(Guid Id, TaskeStatus TaskStatus) : IRequest<Result>;
}
