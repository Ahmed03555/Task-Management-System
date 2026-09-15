using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Model.Comments.Common;
using TaskManagement.Application.Model.Users.Commands.Queries.GetAllUsers;

namespace TaskManagement.Application.Model.Comments.Queries
{
    public record GetTaskCommentsQuery(Guid Id, int PageNamber = 1, int PageSize = 10) : IRequest<Result<PaginatedList<CommentDto>>>;
}
