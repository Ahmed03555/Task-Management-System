using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;
using TaskManagement.Application.Model.Comments.Common;
using TaskManagement.Application.Model.Users.Commands.Queries.GetAllUsers;

namespace TaskManagement.Application.Model.Comments.Queries
{
    public class GetTaskCommentsQueryHandler : IRequestHandler<GetTaskCommentsQuery, Result<PaginatedList<CommentDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetTaskCommentsQueryHandler(IMapper mapper, IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _mapper=mapper;
            _context=context;
            _currentUserService=currentUserService;
        }

        public async Task<Result<PaginatedList<CommentDto>>> Handle(GetTaskCommentsQuery request, CancellationToken cancellationToken)
        {
            var task = await _context.tasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (task is null)
                return Result<PaginatedList<CommentDto>>.Failure("Task not found");

            if (task.Project.OwnerId != _currentUserService.UserId)
                return Result<PaginatedList<CommentDto>>.Failure("You are not authorized to view these comments.");

            var query =  _context.comments.Where(c => c.TaskItemId == request.Id).OrderByDescending(c => c.CreatedAt)
                  .ProjectTo<CommentDto>(_mapper.ConfigurationProvider);

            var page = await PaginatedList<CommentDto>.CreateAsync(query,request.PageNamber,request.PageSize,cancellationToken);

            return Result<PaginatedList<CommentDto>>.Success(page);
        }
    }
}
