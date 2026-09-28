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
using TaskManagement.Application.Model.Projects.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskManagement.Application.Model.Projects.Queries.GetUserProjects
{
    public class GetUserProjectsQueryHandler : IRequestHandler<GetUserProjectsQuery, Result<PaginatedList<ProjectDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        private readonly ICacheService _cache;

        public GetUserProjectsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper,ICacheService cache)
        {
            _context=context;
            _currentUserService=currentUserService;
            _mapper=mapper;
            _cache = cache;
        }

        public async Task<Result<PaginatedList<ProjectDto>>> Handle(
    GetUserProjectsQuery request,
    CancellationToken cancellationToken)
        {
            var cacheKey = $"projects:{_currentUserService.UserId}:{request.OwnerId}:{request.PageNumber}:{request.PageSize}";

            var cache = await _cache.GetAsync<PaginatedList<ProjectDto>>(cacheKey, cancellationToken);

            if (cache is not null)
                return Result<PaginatedList<ProjectDto>>.Success(cache);

            if (_currentUserService.UserId is null)
                return Result<PaginatedList<ProjectDto>>
                    .Failure("User is not authenticated.");


            var query = _context.projects
                .Include(p => p.Owner)
                .AsQueryable();


            if (_currentUserService.IsAdmin)
            {
                if (request.OwnerId.HasValue)
                {
                    query = query.Where(p =>
                        p.OwnerId == request.OwnerId.Value);
                }
            }
            else
            {
                query = query.Where(p =>
                    p.OwnerId == _currentUserService.UserId.Value);
            }
            

            var projected = query
                .OrderByDescending(p => p.CreatedAt)
                .ProjectTo<ProjectDto>(
                    _mapper.ConfigurationProvider);

            

            var paginatedList =
                await PaginatedList<ProjectDto>.CreateAsync(
                    projected,
                    request.PageNumber,
                    request.PageSize,
                    cancellationToken);

            await _cache.SetAsync(cacheKey, paginatedList, TimeSpan.FromMinutes(2), cancellationToken);

            return Result<PaginatedList<ProjectDto>>
                .Success(paginatedList);
        }
    }
    }
