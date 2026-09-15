using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Model.Tasks.Commands.CreateTask
{
    #region CreateTaskCommand
    public record CreateTaskCommand(
string Title,
string? Description,
TaskPriority Priority,
Guid ProjectId
) : IRequest<Result<Guid>>; 
    #endregion
}
