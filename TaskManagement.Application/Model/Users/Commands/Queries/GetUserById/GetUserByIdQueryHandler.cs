using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;
using TaskManagement.Application.Model.Users.Commands.Queries.GetAllUsers;

namespace TaskManagement.Application.Model.Users.Commands.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _applicationDbContext;

        public GetUserByIdQueryHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
        {
            _mapper=mapper;
            _applicationDbContext=applicationDbContext;
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _applicationDbContext.users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user is null)
                return Result<UserDto>.Failure("User not found");

            return Result<UserDto>.Success(_mapper.Map<UserDto>(user));
        }
    }
}
