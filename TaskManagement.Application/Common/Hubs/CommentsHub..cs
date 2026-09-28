using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interface;




namespace TaskManagement.Application.Common.Hubs
{
        [Authorize]
        public class CommentsHub : Hub
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public CommentsHub(
                IApplicationDbContext context,
                ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task JoinTask(Guid taskId)
            {
                var task = await _context.tasks
                    .Include(t => t.Project)
                    .FirstOrDefaultAsync(t => t.Id == taskId);

                if (task is null)
                {
                    throw new HubException("Task not found.");
                }

                var userId = _currentUserService.UserId;

                if (userId is null)
                {
                    throw new HubException("User is not authenticated.");
                }

                var isAllowed =
                    task.Project.OwnerId == userId ||
                    _currentUserService.IsAdmin;

                if (!isAllowed)
                {
                    throw new HubException(
                        "You are not authorized to join this task."
                    );
                }

                await Groups.AddToGroupAsync(
                    Context.ConnectionId,
                    GetTaskGroup(taskId)
                );
            }

            public async Task LeaveTask(Guid taskId)
            {
                await Groups.RemoveFromGroupAsync(
                    Context.ConnectionId,
                    GetTaskGroup(taskId)
                );
            }

            private static string GetTaskGroup(Guid taskId)
            {
                return $"task:{taskId}";
            }
        }
    }

