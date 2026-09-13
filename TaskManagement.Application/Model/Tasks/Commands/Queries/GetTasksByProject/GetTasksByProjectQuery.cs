using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Tasks.Commands.Queries.GetTasksByProject
{
    #region GetTasksByProjectQuery
    public record GetTasksByProjectQuery(Guid ProjectId, int PageNumber = 1,
    int PageSize = 10) : IRequest<Result<PaginatedList<TaskDto>>>; 
	#endregion
}
