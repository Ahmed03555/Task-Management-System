using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Model.Tasks.Commands.CreateTask
{
    #region CreateTaskCommandHandler
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result<Guid>>
    {
        private readonly IApplicationDbContext _dbContext;
        public CreateTaskCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<Guid>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var projectExists = await _dbContext.projects.AnyAsync(p => p.Id == request.ProjectId, cancellationToken);

            if (!projectExists)
                return Result<Guid>.Failure("Project is not Found");

            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                ProjectId = request.ProjectId,
                CreatedAt = DateTime.UtcNow,

            };
            await _dbContext.tasks.AddAsync(task, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(task.Id);
        }
    } 
    #endregion
}
