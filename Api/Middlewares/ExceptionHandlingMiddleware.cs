using Api.Models;
using Newtonsoft.Json;

namespace Api.Middlewares
{
    // Custom exception handling middleware
    // why: To unify the error handeling for all API endpoints 
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new ApiResponse<object>
            {
                Data = null,
                Success = false,
                Error = "An error occurred while processing your request.",
                Message = exception.Message
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
