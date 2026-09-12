using System.Net;
using System.Text.Json;
using TaskManagement.Application.Common.Exceptions;

namespace Task_Management_System.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            #region ClientErrorHandling
            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var response = new
                {
                    title = "One or more validation failures occurred.",
                    status = 400,
                    errors = ex.Errors
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            #endregion
            #region ServerErrorHandling
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new
                {
                    title = "An unexpected error occurred.",
                    status = 500,
                    detail = ex.Message
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            } 
            #endregion
        }
    }
}
