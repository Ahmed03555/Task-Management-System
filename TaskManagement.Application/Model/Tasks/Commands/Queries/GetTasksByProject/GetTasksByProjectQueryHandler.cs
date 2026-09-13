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

namespace TaskManagement.Application.Model.Tasks.Commands.Queries.GetTasksByProject
{
    #region GetTasksByProjectQueryHandler
    public class GetTasksByProjectQueryHandler : IRequestHandler<GetTasksByProjectQuery, Result<PaginatedList<TaskDto>>>
    {
        private readonly IApplicationDbContext _ApplicationDbContext;
        private readonly IMapper _Mapper;

        public GetTasksByProjectQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _ApplicationDbContext=applicationDbContext;
            _Mapper=mapper;
        }

        public async Task<Result<PaginatedList<TaskDto>>> Handle(GetTasksByProjectQuery request, CancellationToken cancellationToken)
        {
            var tasks =  _ApplicationDbContext.tasks.Where(t => t.ProjectId == request.ProjectId).OrderByDescending(t => t.CreatedAt).ProjectTo<TaskDto>(_Mapper.ConfigurationProvider);
            var paginatedTasks = await PaginatedList<TaskDto>.CreateAsync(tasks, request.PageNumber, request.PageSize, cancellationToken);

            return Result<PaginatedList<TaskDto>>.Success(paginatedTasks);
        }
    } 
    #endregion
}
