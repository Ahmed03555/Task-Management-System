using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;

namespace TaskManagement.Application.Model.Tasks.Commands.UpdateTaskStatus
{
    public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand, Result>
    {
        private readonly IApplicationDbContext _taskRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTaskStatusCommandHandler(IApplicationDbContext taskRepository, ICurrentUserService currentUserService)
        {
            _taskRepository=taskRepository;
            _currentUserService=currentUserService;
        }

        public async Task<Result> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.tasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if(task is null)
                return Result.Failure($"Task with Id {request.Id} not found.");
            
            if(task.Project.OwnerId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                return Result.Failure($"You are not authorized to update the status of this task.");

            task.Satus = request.TaskStatus;
            task.UpdatedAt = DateTime.UtcNow;

            await _taskRepository.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
