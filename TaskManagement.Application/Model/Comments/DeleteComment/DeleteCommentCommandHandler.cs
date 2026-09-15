using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;

namespace TaskManagement.Application.Model.Comments.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result>
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly ICurrentUserService currentUserService;

        public DeleteCommentCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
        {
            this.applicationDbContext=applicationDbContext;
            this.currentUserService=currentUserService;
        }

        public async Task<Result> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await applicationDbContext.comments.FirstOrDefaultAsync(c => c.Id ==request.Id, cancellationToken);
            if (comment is null)
                return Result.Failure("Comment not found");

            if (comment.AuthorId != currentUserService.UserId)
                return Result.Failure("You are not authorized to delete this comment.");

            applicationDbContext.comments.Remove(comment);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
