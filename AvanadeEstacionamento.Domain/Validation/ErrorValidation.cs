using System.Net;

namespace AvanadeEstacionamento.Domain.Validation
{
    public class ErrorValidation
    {
        public ErrorValidation()
        {
            TraceId = Guid.NewGuid().ToString();
            Errors = new List<ErrorDetails>();
        }

        public ErrorValidation(int statusCode, string message, HttpStatusCode status)
        {
            TraceId = Guid.NewGuid().ToString();
            Errors = new List<ErrorDetails>();
            AddError(statusCode, message, status);
        }

        public string TraceId { get; private set; }
        public List<ErrorDetails> Errors { get; private set; }

        public class ErrorDetails
        {
            public ErrorDetails(int statusCode, string message, HttpStatusCode status)
            {
                Logref = statusCode;
                Message = message;
                Status = status;
            }

            public int Logref { get; private set; }
            public string Message { get; private set; }
            public HttpStatusCode Status { get; set; }
        }

        public void AddError(int statusCode, string message, HttpStatusCode status)
        {
            Errors.Add(new ErrorDetails(statusCode, message, status));
        }
    }
}
