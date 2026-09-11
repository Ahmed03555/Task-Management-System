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

namespace TaskManagement.Application.Model.Tasks.Commands.Queries.GetTasksByProject
{
    #region GetTasksByProjectQueryHandler
    public class GetTasksByProjectQueryHandler : IRequestHandler<GetTasksByProjectQuery, Result<List<TaskDto>>>
    {
        private readonly IApplicationDbContext _ApplicationDbContext;
        private readonly IMapper _Mapper;

        public GetTasksByProjectQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _ApplicationDbContext=applicationDbContext;
            _Mapper=mapper;
        }

        public async Task<Result<List<TaskDto>>> Handle(GetTasksByProjectQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _ApplicationDbContext.tasks.Where(t => t.ProjectId == request.ProjectId).ProjectTo<TaskDto>(_Mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            return Result<List<TaskDto>>.Success(tasks);
        }
    } 
    #endregion
}
