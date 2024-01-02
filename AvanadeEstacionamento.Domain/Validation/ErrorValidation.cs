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

        public ErrorValidation(string message, HttpStatusCode status)
        {
            TraceId = Guid.NewGuid().ToString();
            Errors = new List<ErrorDetails>();
            AddError(message, status);
        }

        public string TraceId { get; private set; }
        public List<ErrorDetails> Errors { get; private set; }

        public class ErrorDetails
        {
            public string Message { get; private set; }
            public HttpStatusCode Status { get; set; }

            public ErrorDetails(string message, HttpStatusCode status)
            {
                Message = message;
                Status = status;
            }
        }

        public void AddError(string message, HttpStatusCode status)
        {
            Errors.Add(new ErrorDetails(message, status));
        }
    }
}
