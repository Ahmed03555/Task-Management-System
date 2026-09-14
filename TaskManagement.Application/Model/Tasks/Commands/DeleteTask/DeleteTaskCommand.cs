using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Tasks.Commands.DeleteTask
{
    public record DeleteTaskCommand(Guid Id) : IRequest<Result>;
}
