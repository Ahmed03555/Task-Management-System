using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Model.Users.Commands.RegisterUser;

namespace Task_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegisterUserController(IMediator mediator)
        {
            _mediator=mediator;
        }
        #region RegisterUser

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        } 
        #endregion
    }
}
