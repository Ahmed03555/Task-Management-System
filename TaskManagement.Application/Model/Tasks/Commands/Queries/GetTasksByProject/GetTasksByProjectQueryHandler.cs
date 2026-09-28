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

        private readonly ICacheService _cacheService;

        public GetTasksByProjectQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper,ICacheService cacheService)
        {
            _ApplicationDbContext=applicationDbContext;
            _Mapper=mapper;
            _cacheService = cacheService;
        }

        public async Task<Result<PaginatedList<TaskDto>>> Handle(GetTasksByProjectQuery request, CancellationToken cancellationToken)
        {
            #region getRedis

            var cacheKey = $"tasks:project:{request.ProjectId}:page:{request.PageNumber}:size:{request.PageSize}"; ;

            var cached = await _cacheService.GetAsync<PaginatedList<TaskDto>>(cacheKey, cancellationToken);

            if (cached is not null)
            {
                return Result<PaginatedList<TaskDto>>.Success(cached);
            }

            #endregion
            var tasks =  _ApplicationDbContext.tasks.Where(t => t.ProjectId == request.ProjectId).OrderByDescending(t => t.CreatedAt).ProjectTo<TaskDto>(_Mapper.ConfigurationProvider);
            var paginatedTasks = await PaginatedList<TaskDto>.CreateAsync(tasks, request.PageNumber, request.PageSize, cancellationToken);

            #region SetRedis
            await _cacheService.SetAsync(cacheKey, paginatedTasks, TimeSpan.FromMinutes(7), cancellationToken);
            #endregion

            return Result<PaginatedList<TaskDto>>.Success(paginatedTasks);
        }
    } 
    #endregion
}
