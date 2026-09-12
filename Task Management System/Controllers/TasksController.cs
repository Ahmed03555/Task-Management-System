using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Model.Tasks.Commands.CreateTask;
using TaskManagement.Application.Model.Tasks.Commands.Queries.GetTasksByProject;

namespace Task_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    #region When To Authorize Section
    //[Authorize] 
    #endregion
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Create

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        } 
        #endregion

        #region GetTasksByProjectId

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetTasksByProjectId(Guid projectId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTasksByProjectQuery(projectId), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        } 
        #endregion
    }
}
