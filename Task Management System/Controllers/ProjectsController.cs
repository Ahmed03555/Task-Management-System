using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Model.Projects.Commands.CreateProject;
using TaskManagement.Application.Model.Projects.Commands.DeleteProject;
using TaskManagement.Application.Model.Projects.Commands.UpdateProject;
using TaskManagement.Application.Model.Projects.Queries;
using TaskManagement.Application.Model.Projects.Queries.GetProjectById;
using TaskManagement.Application.Model.Projects.Queries.GetUserProjects;

namespace Task_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly ISender _mediator;

        public ProjectsController(ISender mediator)
        {
            _mediator=mediator;
        }

        #region CreateProject
        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        #endregion

        #region GetUserProjects
        [HttpGet]
        public async Task<IActionResult> GetMyProjects([FromQuery] int pageNumber = 1,
         [FromQuery] int pageSize = 10, [FromQuery] Guid? ownerId = null, CancellationToken ct = default)
        {
            var result = await _mediator.Send(new GetUserProjectsQuery(pageNumber, pageSize, ownerId), ct);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        #endregion
        #region UpdateProject

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(Guid id, UpdateProjectCommand command, CancellationToken ct)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch.");

            var result = await _mediator.Send(command, ct);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        #endregion

        #region DeleteProject
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new DeleteProjectCommand(id), ct);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
        #endregion

        #region GetProjectById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetProjectByIdQuery(id), ct);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        #endregion

    }
}
