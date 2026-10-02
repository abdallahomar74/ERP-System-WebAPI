using DomainLayer.Exceptions;
using Shared.ErrorModels;

namespace ERPSystem.Web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            _next = Next;
            this._logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                await HandleNotFoundEndPointAsync(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went Wrong");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            if (ex is BadRequestException badRequestException)
            {
                await WriteValidationResponse(context, badRequestException.Errors);
                return;
            }

            var response = new ErrorToReturn()
            {
                ErrorMessage = ex.Message,
                StatusCode = ex switch
                {
                    NotFoundExpceptions => StatusCodes.Status404NotFound,
                    UnauthorizedException => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError
                }
            };

            context.Response.StatusCode = response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task WriteValidationResponse(HttpContext context, List<DomainLayer.Exceptions.ValidationError> errors)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new ValidationErrorToReturn
            {
                Errors = errors.Select(e => new Shared.ErrorModels.ValidationError
                {
                    Field = e.Field,
                    Errors = e.Errors
                })
            };

            await context.Response.WriteAsJsonAsync(response);
        }


        private static async Task HandleNotFoundEndPointAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End point you call = {context.Request.Path} is Not Found! "
                };
                await context.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
