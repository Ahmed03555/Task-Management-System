using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Interface;

namespace TaskManagement.Application.Model.Users.Commands.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<PaginatedList<UserDto>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
       

        private readonly IMapper mapper;

        public GetAllUsersQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext=applicationDbContext;
            this.mapper=mapper;
        }

        public async Task<Result<PaginatedList<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var user =  _applicationDbContext.users.OrderByDescending(u => u.CreatedAt).ProjectTo<UserDto>(mapper.ConfigurationProvider);
            var page = await PaginatedList<UserDto>.CreateAsync(user, request.PageNumber, request.PageSize, cancellationToken);

            return Result<PaginatedList<UserDto>>.Success(page);
        }
    }
}
