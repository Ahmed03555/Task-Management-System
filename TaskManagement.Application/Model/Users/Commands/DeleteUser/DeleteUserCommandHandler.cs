using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;

namespace TaskManagement.Application.Model.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        private readonly ICacheService _cacheService;

        public DeleteUserCommandHandler(IApplicationDbContext context, ICacheService cacheService)
        {
            _context=context;
            _cacheService =cacheService;
            
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            
            var user = await _context.users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user is null)
                return Result.Failure("User is not found");

            _context.users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            await _cacheService.SetAsync("users:version", Guid.NewGuid().ToString(), TimeSpan.FromDays(7), cancellationToken);

            await _cacheService.RemoveAsync($"user:{request.Id}", cancellationToken);
            return Result.Success();
        }
    }
}
