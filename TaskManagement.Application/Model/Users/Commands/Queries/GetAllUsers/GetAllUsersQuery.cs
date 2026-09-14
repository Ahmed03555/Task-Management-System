using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Users.Commands.Queries.GetAllUsers
{
    public record GetAllUsersQuery(int PageNumber = 1,
        int PageSize = 10) : IRequest<Result<PaginatedList<UserDto>>>;
    
    
}
