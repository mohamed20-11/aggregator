using System.Text.Json;

namespace DynamicWorkflow.Aggregator.Middleware
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IResponseHelper _responseHelper;

        public GlobalExceptionMiddleware
        (
            ILogger<GlobalExceptionMiddleware> logger,
            IResponseHelper responseHelper
        )
        {
            _logger = logger;
            _responseHelper = responseHelper;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                _responseHelper.Exception();

                var serializedResponse = JsonSerializer.Serialize(_responseHelper.Exception());
                await context.Response.WriteAsync(serializedResponse);
            }
        }
    }
}
