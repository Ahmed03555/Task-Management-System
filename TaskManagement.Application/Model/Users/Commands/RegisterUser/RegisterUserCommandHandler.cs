using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Model.Users.Commands.RegisterUser
{
    #region RegisterUserCommandHandler
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserCommandHandler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher)
        {
            _dbContext=dbContext;
            _passwordHasher=passwordHasher;
        }

        public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _dbContext.users.AnyAsync(u => u.Email == request.Email, cancellationToken);

            if (emailExists)
                return Result<Guid>.Failure("Email already exists.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = _passwordHasher.Hash(request.Password)
            };

            await _dbContext.users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(user.Id);

        } 
    #endregion
    }
}
