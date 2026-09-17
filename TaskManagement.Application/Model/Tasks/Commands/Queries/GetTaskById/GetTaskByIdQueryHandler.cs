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

        public GetTaskByIdQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
        {
            _context=context;
            _currentUserService=currentUserService;
            _mapper=mapper;
        }

        public async Task<Result<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _context.tasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (task == null)
                return Result<TaskDto>.Failure("Task not found");

            if(task.Project.OwnerId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                return Result<TaskDto>.Failure("You are not authorized to view this task");

            return Result<TaskDto>.Success(_mapper.Map<TaskDto>(task));
        }
    }
}
