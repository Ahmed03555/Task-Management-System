using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;
using TaskManagement.Application.Model.Projects.Common;

namespace TaskManagement.Application.Model.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Result<ProjectDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICacheService _cacheServices;

        public GetProjectByIdQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService, ICacheService cacheService)
        {
            _context=context;
            _mapper=mapper;
            _currentUserService=currentUserService;
            _cacheServices = cacheService;
        }

        public async Task<Result<ProjectDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"{request.Id}";

            var cached = await _cacheServices.GetAsync<ProjectDto>(cacheKey, cancellationToken);

            if(cached is not null)
            {
                if (cached.OwnerId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                {
                    return Result<ProjectDto>.Failure("You are not authorized to access this project");
                }

                return Result<ProjectDto>.Success(cached);
            }

            var project = await _context.projects.Include(p => p.Owner).FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if(project is null)
                return Result<ProjectDto>.Failure("Project not found");

            if(project.OwnerId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                return Result<ProjectDto>.Failure("You are not authorized to access this project");
            
            var projectDto = _mapper.Map<ProjectDto>(project);

            await _cacheServices.SetAsync(cacheKey, projectDto, TimeSpan.FromMinutes(5), cancellationToken);

            return Result<ProjectDto>.Success(projectDto);
        }
    }
}
