using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Model.Tasks.Commands.Queries.GetTasksByProject
{
    #region GetTasksByProjectQuery
    public record GetTasksByProjectQuery(Guid ProjectId) : IRequest<Result<List<TaskDto>>>; 
	#endregion
}
