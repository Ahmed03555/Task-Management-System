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

namespace TaskManagement.Application.Model.Projects.Queries.GetUserProjects
{
    public class GetUserProjectsQueryHandler : IRequestHandler<GetUserProjectsQuery, Result<List<ProjectDto>>>
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

        public async Task<Result<List<ProjectDto>>> Handle(GetUserProjectsQuery request, CancellationToken cancellationToken)
        {
             if(_currentUserService.UserId is null)
                return Result<List<ProjectDto>>.Failure("User is not authenticated.");

            var projects = await _context.projects
               .Where(p => p.OwnerId == _currentUserService.UserId)
               .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);

            return Result<List<ProjectDto>>.Success(projects);
        }
    }
}
