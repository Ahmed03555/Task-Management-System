using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Comments.AddComment
{
    public record AddCommentCommand (Guid TaskItemId,string Content) : IRequest<Result<Guid>>
    {
    }
}
