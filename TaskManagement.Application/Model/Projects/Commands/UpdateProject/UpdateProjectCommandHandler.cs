using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;

namespace TaskManagement.Application.Model.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Result<Guid>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateProjectCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context=context;
            _currentUserService=currentUserService;
        }

        public async Task<Result<Guid>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.projects.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if(entity is null)
                return Result<Guid>.Failure("Project not found.");

            if (entity.OwnerId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                return Result<Guid>.Failure("You are not authorized to update this project.");

            entity.ProjectName = request.Name;
            entity.Description = request.Description;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(entity.Id);
        }
    }
}
