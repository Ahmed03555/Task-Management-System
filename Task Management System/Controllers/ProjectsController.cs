using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Model.Projects.Commands.CreateProject;
using TaskManagement.Application.Model.Projects.Queries;
using TaskManagement.Application.Model.Projects.Queries.GetUserProjects;

namespace Task_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
        public async Task<IActionResult> GetMyProjects(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUserProjectsQuery(), ct);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        #endregion
    }
}
