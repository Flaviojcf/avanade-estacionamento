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
                errorValidation = new ErrorValidation((int)HttpStatusCode.NotFound,
                                     $"{ex.Message} {ex?.InnerException?.Message}", HttpStatusCode.NotFound);
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                errorValidation = new ErrorValidation((int)HttpStatusCode.InternalServerError,
                                                     "Ocorreu um erro interno, entre em contato.", HttpStatusCode.InternalServerError);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

           

            var result = JsonSerializer.Serialize(errorValidation);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}
