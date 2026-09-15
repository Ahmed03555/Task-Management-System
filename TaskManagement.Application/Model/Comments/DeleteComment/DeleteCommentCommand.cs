using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Comments.DeleteComment
{
    public record DeleteCommentCommand(Guid Id) : IRequest<Result>;
}
