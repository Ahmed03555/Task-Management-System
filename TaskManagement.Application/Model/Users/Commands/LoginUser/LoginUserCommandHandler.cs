using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;

namespace TaskManagement.Application.Model.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IApplicationDbContext _applicationDbContext;

        private readonly IPasswordHasher _passwordHasher;

        public LoginUserCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IApplicationDbContext applicationDbContext, IPasswordHasher passwordHasher)
        {
            _jwtTokenGenerator=jwtTokenGenerator;
            _applicationDbContext=applicationDbContext;
            _passwordHasher=passwordHasher;
        }

        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _applicationDbContext.users.FirstOrDefaultAsync(u => u.Email == request.Email,cancellationToken);
            if(user is null)
                return Result<string>.Failure("Invalid email or password.");
            

            var isPasswordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if(!isPasswordValid)
                return Result<string>.Failure("Invalid email or password.");

            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email,user.Role.ToString());

            return Result<string>.Success(token);
        }
    }
}
