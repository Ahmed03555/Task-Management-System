using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(
        string FullName,
        string Email,
        string Password
    ) : IRequest<Result<Guid>>;
    
    
}
