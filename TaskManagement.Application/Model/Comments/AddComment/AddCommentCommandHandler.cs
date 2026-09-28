using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Hubs;
using TaskManagement.Application.Common.Interface;
using TaskManagement.Application.Model.Comments.Common;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Model.Comments.AddComment
{
    public class AddCommentCommandHandler
        : IRequestHandler<AddCommentCommand, Result<Guid>>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly ICurrentUserService _currentUserService;
        private readonly IHubContext<CommentsHub> _hubContext;
        private readonly IMapper _mapper;

        public AddCommentCommandHandler(
            IApplicationDbContext applicationDb,
            ICurrentUserService currentUserService,
            IHubContext<CommentsHub> hubContext,
            IMapper mapper)
        {
            _applicationDb = applicationDb;
            _currentUserService = currentUserService;
            _hubContext = hubContext;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(
            AddCommentCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
            {
                return Result<Guid>.Failure(
                    "User is not authenticated.");
            }

            var task = await _applicationDb.tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(
                    t => t.Id == request.TaskItemId,
                    cancellationToken);

            if (task is null)
            {
                return Result<Guid>.Failure("Task not found");
            }

            if (task.Project.OwnerId != _currentUserService.UserId &&
                !_currentUserService.IsAdmin)
            {
                return Result<Guid>.Failure(
                    "You are not authorized to comment on this task.");
            }

            var comment = new Comment
            {
                CreatedAt = DateTime.UtcNow,
                Content = request.Content,
                TaskItemId = request.TaskItemId,
                AuthorId = _currentUserService.UserId.Value
            };

            await _applicationDb.comments.AddAsync(
                comment,
                cancellationToken);

            await _applicationDb.SaveChangesAsync(cancellationToken);

            // تحويل الـ Comment إلى DTO
            var commentDto = await _applicationDb.comments
                .Where(c => c.Id == comment.Id)
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .FirstAsync(cancellationToken);

            // إرسال الـ Comment لكل الموجودين في نفس Task
            await _hubContext.Clients
                .Group($"task:{comment.TaskItemId}")
                .SendAsync(
                    "CommentAdded",
                    commentDto,
                    cancellationToken);

            return Result<Guid>.Success(comment.Id);
        }
    }
}