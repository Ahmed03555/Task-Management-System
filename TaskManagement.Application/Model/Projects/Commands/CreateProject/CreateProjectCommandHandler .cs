using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Model.Projects.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<Guid>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _applicationDbContext;

        public CreateProjectCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext)
        {
            _currentUserService=currentUserService;
            _applicationDbContext=applicationDbContext;
        }

        public async Task<Result<Guid>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            if(_currentUserService.UserId is null)
                return Result<Guid>.Failure("User is not authenticated.");

            var project = new Project
            {
                ProjectName = request.Name,
                Description = request.Description,
                OwnerId = _currentUserService.UserId.Value,
                CreatedAt = DateTime.UtcNow
            };
            await _applicationDbContext.projects.AddAsync(project, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(project.Id);
        }
    }
}
