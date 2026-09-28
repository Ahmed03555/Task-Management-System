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

        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public GetAllUsersQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper,ICacheService cacheService)
        {
            _applicationDbContext=applicationDbContext;
            this.mapper=mapper;
            this.cacheService = cacheService;
        }

        public async Task<Result<PaginatedList<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var search = request.Search?.Trim().ToLower() ?? string.Empty;

            var version = await cacheService.GetAsync<string>("users:version", cancellationToken) ?? "0";

            var cacheKey = $"users:list:{version}:{request.PageNumber}:{request.PageSize}:{search}";
            #region Search

            var query = _applicationDbContext.users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
             
                query = query.Where(u => u.FullName.ToLower().Contains(search) || u.Email.ToLower().Contains(search));
            }
            #endregion



            var projected = query.OrderByDescending(u => u.CreatedAt).ProjectTo<UserDto>(mapper.ConfigurationProvider);
            var page = await PaginatedList<UserDto>.CreateAsync(projected, request.PageNumber, request.PageSize, cancellationToken);
            await cacheService.SetAsync(cacheKey, page, TimeSpan.FromMinutes(2), cancellationToken);
            return Result<PaginatedList<UserDto>>.Success(page);
        }
    }
}
