using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;

namespace TaskManagement.Application.Model.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        private readonly ICacheService _cacheService;

        public DeleteProjectCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService,ICacheService cacheService)
        {
            _context=context;
            _currentUserService=currentUserService;
            _cacheService=cacheService;
        }

        public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.projects.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if(project is null)
                return Result.Failure($"Project with Id {request.Id} not found.");

            if(project.OwnerId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                return Result.Failure("You are not authorized to delete this project.");

            _context.projects.Remove(project);
            await _context.SaveChangesAsync(cancellationToken);

            await _cacheService.RemoveAsync($"project:{request.Id}", cancellationToken);
            return Result.Success();
        }
    }
}
