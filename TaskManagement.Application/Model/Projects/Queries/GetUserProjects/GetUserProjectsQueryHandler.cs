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

        public GetUserProjectsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
        {
            _context=context;
            _currentUserService=currentUserService;
            _mapper=mapper;
        }

        public async Task<Result<PaginatedList<ProjectDto>>> Handle(GetUserProjectsQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                return Result<PaginatedList<ProjectDto>>.Failure("User is not authenticated.");

            var query = _context.projects.AsQueryable();

            if (_currentUserService.IsAdmin)
            {
                if (request.OwnerId.HasValue)
                    query = query.Where(p => p.OwnerId == request.OwnerId.Value);
                
            }
            else
            {
                query = query.Where(p => p.OwnerId == _currentUserService.UserId.Value);
            }

            var projected = query
                .OrderByDescending(p => p.CreatedAt)
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider);   

            var paginatedList = await PaginatedList<ProjectDto>.CreateAsync(
                projected, request.PageNumber, request.PageSize, cancellationToken);

            return Result<PaginatedList<ProjectDto>>.Success(paginatedList);
        }
    }
}
