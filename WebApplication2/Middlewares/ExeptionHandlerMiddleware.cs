using System.Net;

namespace Web_Api.Middlewares
{
    public class ExeptionHandlerMiddleware
    {
        private readonly ILogger<ExeptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExeptionHandlerMiddleware(ILogger<ExeptionHandlerMiddleware>logger,RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);

            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                // log this exption
                logger.LogError(ex,$"{errorId} :{ ex.Message}" );

                // return custom error responce 
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage= "something going wrong we are lokking into this"
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
