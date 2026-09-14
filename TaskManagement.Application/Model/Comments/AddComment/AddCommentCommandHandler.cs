using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Model.Comments.AddComment
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Result<Guid>>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly ICurrentUserService _currentUserService;

        public AddCommentCommandHandler(IApplicationDbContext applicationDb, ICurrentUserService currentUserService)
        {
            _applicationDb=applicationDb;
            _currentUserService=currentUserService;
        }

        public async Task<Result<Guid>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var task = await _applicationDb.tasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id ==request.TaskItemId, cancellationToken);

            if (task is null)
                return Result<Guid>.Failure("Task not found");

            if (task.Project.OwnerId != _currentUserService.UserId)
                return Result<Guid>.Failure("You are not authorized to comment on this task.");

            var comment = new Comment
            {
                CreatedAt = DateTime.Now,
                Content = request.Content,
                TaskItemId = request.TaskItemId,
                AuthorId = _currentUserService.UserId!.Value

            };

            await _applicationDb.comments.AddAsync(comment,cancellationToken);
            await _applicationDb.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(comment.Id);
        }
    }
}
