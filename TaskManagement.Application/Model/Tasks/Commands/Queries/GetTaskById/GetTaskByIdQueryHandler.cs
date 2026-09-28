using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;
using TaskManagement.Application.Model.Tasks.Commands.Queries.GetTasksByProject;

namespace TaskManagement.Application.Model.Tasks.Commands.Queries.GetTaskById
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, Result<TaskDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        private readonly ICacheService _cacheService;

        public GetTaskByIdQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper,ICacheService cacheService)
        {
            _context=context;
            _currentUserService=currentUserService;
            _mapper=mapper;
            _cacheService=cacheService;
        }

        public async Task<Result<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            #region GetRedis
            var cacheKey = $"tasks:byid:{request.Id}";

            var cached = await _cacheService.GetAsync<TaskDto>(cacheKey, cancellationToken);

            if (cached is not null)
            {

                if (cached.ProjectOwnerId != _currentUserService.UserId
                    && !_currentUserService.IsAdmin)
                {
                    return Result<TaskDto>.Failure(
                        "You are not authorized to view this task");
                }
                return Result<TaskDto>.Success(cached);
            } 
            #endregion

            var task = await _context.tasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (task == null)
                return Result<TaskDto>.Failure("Task not found");

            if(task.Project.OwnerId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                return Result<TaskDto>.Failure("You are not authorized to view this task");

            #region setRedis
            await _cacheService.SetAsync(cacheKey, _mapper.Map<TaskDto>(task), TimeSpan.FromMinutes(5), cancellationToken);

            #endregion
            return Result<TaskDto>.Success(_mapper.Map<TaskDto>(task));
        }
    }
}
