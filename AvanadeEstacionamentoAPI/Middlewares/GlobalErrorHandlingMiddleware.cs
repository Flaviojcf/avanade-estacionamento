using AvanadeEstacionamento.API.EstacionamentoConstants;
using AvanadeEstacionamento.Domain.Exceptions;
using AvanadeEstacionamento.Domain.Validation;
using System.Net;
using System.Text.Json;

namespace AvanadeEstacionamento.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public GlobalErrorHandlingMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ErrorValidation errorValidation;

            var exceptionType = ex.GetType();

            if (exceptionType == typeof(NotFoundException))
            {
                errorValidation = new ErrorValidation($"{ex.Message} {ex?.InnerException?.Message}", HttpStatusCode.NotFound);
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (exceptionType == typeof(ResourceAlreadyExistsException))
            {
                errorValidation = new ErrorValidation($"{ex.Message} {ex?.InnerException?.Message}", HttpStatusCode.Conflict);
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            }
            else if (exceptionType == typeof(ArgumentException))
            {
                errorValidation = new ErrorValidation($"{ex.Message} {ex?.InnerException?.Message}", HttpStatusCode.BadRequest);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                errorValidation = new ErrorValidation(AvanadeEstacionamentoConstants.GLOBAL_MESSAGE_FOR_INTERNAL_ERROR_EXCEPTION, HttpStatusCode.InternalServerError);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            var result = JsonSerializer.Serialize(errorValidation);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}
