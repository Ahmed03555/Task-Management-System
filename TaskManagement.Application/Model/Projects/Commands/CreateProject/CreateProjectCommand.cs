using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Projects.Commands.CreateProject
{
    public record CreateProjectCommand(string Name,
        string? Description , Guid? OwnerId = null) : IRequest<Result<Guid>>
    {
    }
}
