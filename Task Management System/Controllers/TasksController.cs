using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Model.Tasks.Commands.CreateTask;
using TaskManagement.Application.Model.Tasks.Commands.DeleteTask;
using TaskManagement.Application.Model.Tasks.Commands.Queries.GetTaskById;
using TaskManagement.Application.Model.Tasks.Commands.Queries.GetTasksByProject;
using TaskManagement.Application.Model.Tasks.Commands.UpdateTaskStatus;

namespace Task_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    #region When To Authorize Section
    [Authorize]
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
        public async Task<IActionResult> GetTasksByProjectId(Guid projectId, [FromQuery] int pageNumber = 1,
         [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetTasksByProjectQuery(projectId, pageNumber, pageSize), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
        #endregion


        #region UpdateTaskStatusCommand
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateTaskStatus(Guid id , UpdateTaskStatusCommand command , CancellationToken cancellationToken)
        {
            if(id != command.Id)
            {
                return BadRequest("Task ID mismatch.");
            }
            var result = await _mediator.Send(command, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return NoContent();
        }
        #endregion

        #region DeleteTask
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteTaskCommand(id), cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return NoContent();
        }
        #endregion

        #region GetTaskById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetTaskByIdQuery(id), ct);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        #endregion
    }
}
