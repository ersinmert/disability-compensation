using DisabilityCompensation.Shared.Dtos.Bases;
using DisabilityCompensation.Shared.Exceptions;
using System.Text.Json;

namespace DisabilityCompensation.API.Extensions.Middleware
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ValidationExceptionCustom ex)
            {
                context.Response.ContentType = "application/json";
                await JsonSerializer.SerializeAsync(context.Response.Body, new BaseResponse<object> { Message = "Validation Errors", ValidationErrors = ex.Errors });
            }
        }
    }
}
