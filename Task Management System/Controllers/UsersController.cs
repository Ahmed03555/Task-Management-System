using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Model.Users.Commands.DeleteUser;
using TaskManagement.Application.Model.Users.Commands.LoginUser;
using TaskManagement.Application.Model.Users.Commands.Queries.GetAllUsers;
using TaskManagement.Application.Model.Users.Commands.RegisterUser;

namespace Task_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator=mediator;
        }
        #region RegisterUser

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        #endregion


        #region Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        #endregion


        #region DeleteUser
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id), ct);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
        #endregion

        #region GetAllUser
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers([FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10,CancellationToken ct = default)
        {
            var result = await _mediator.Send(new GetAllUsersQuery(pageNumber, pageSize), ct);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        #endregion
    }
}
