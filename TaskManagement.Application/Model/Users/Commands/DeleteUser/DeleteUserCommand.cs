using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(Guid Id) : IRequest<Result>;

}
