using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;

namespace TaskManagement.Application.Model.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteTaskCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context=context;
            _currentUserService=currentUserService;
        }

        public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.tasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if(task is null)
                return Result.Failure("Task not found.");

            if (task.Project.OwnerId != _currentUserService.UserId)
            {
                return Result.Failure("You are not authorized to delete this task.");

            }

            _context.tasks.Remove(task);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
