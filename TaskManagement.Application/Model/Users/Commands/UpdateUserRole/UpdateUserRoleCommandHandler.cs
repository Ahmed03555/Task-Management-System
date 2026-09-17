using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;

namespace TaskManagement.Application.Model.Users.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, Result>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;

        public UpdateUserRoleCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
        {
            _applicationDbContext=applicationDbContext;
            _currentUserService=currentUserService;
        }

        public async Task<Result> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == _currentUserService.UserId)
                return Result.Failure("You can't change your own role.");

            var user = await _applicationDbContext.users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user is null)
                return Result.Failure("User not found");

            user.Role = request.Role;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
