using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Model.Comments.AddComment;
using TaskManagement.Application.Model.Comments.DeleteComment;
using TaskManagement.Application.Model.Comments.Queries;

namespace Task_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ISender _sender;

        public CommentsController(ISender sender)
        {
            _sender=sender;
        }

        #region AddComment
        [HttpPost]
        public async Task<IActionResult> Add(AddCommentCommand command,CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error
                );
        }
        #endregion

        #region DeleteComment
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id,CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new DeleteCommentCommand(id), cancellationToken);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
        #endregion

        #region GetTaskComments
        [HttpGet("task/{taskItemId}")]
        public async Task<IActionResult> GetByTask(
           Guid taskItemId,
           [FromQuery] int pageNumber = 1,
           [FromQuery] int pageSize = 10,
           CancellationToken ct = default)
        {
            var result = await _sender.Send(new GetTaskCommentsQuery(taskItemId, pageNumber, pageSize), ct);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        #endregion
    }
}
