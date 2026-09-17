using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Model.Users.Commands.Queries.GetAllUsers;

namespace TaskManagement.Application.Model.Users.Commands.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserDto>>;
}
