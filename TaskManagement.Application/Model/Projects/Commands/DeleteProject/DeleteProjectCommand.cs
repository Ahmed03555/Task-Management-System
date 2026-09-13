using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Projects.Commands.DeleteProject
{
    public record DeleteProjectCommand(Guid Id) : IRequest<Result>;

}
