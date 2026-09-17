using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Model.Users.Commands.UpdateUserRole
{
    public record UpdateUserRoleCommand(Guid Id, UserRole Role) : IRequest<Result>;
}
