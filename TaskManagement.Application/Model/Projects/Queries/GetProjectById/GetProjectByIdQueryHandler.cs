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

        public GetProjectByIdQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context=context;
            _mapper=mapper;
            _currentUserService=currentUserService;
        }

        public async Task<Result<ProjectDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.projects.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if(project is null)
                return Result<ProjectDto>.Failure("Project not found");

            if(project.OwnerId != _currentUserService.UserId)
                return Result<ProjectDto>.Failure("You are not authorized to access this project");
            
            var projectDto = _mapper.Map<ProjectDto>(project);
            return Result<ProjectDto>.Success(projectDto);
        }
    }
}
