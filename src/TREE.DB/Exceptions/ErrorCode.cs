using System.Net;

namespace TREE.DB.Exceptions
{
    public class ErrorCode
    {
        public string Message { get; protected set; }
        public HttpStatusCode StatusCode { get; protected set; }

        public ErrorCode(string message, HttpStatusCode statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public static ErrorCode BadRequest => new ErrorCode("Bad Request", HttpStatusCode.BadRequest);
        public static ErrorCode NotFound => new ErrorCode("Not Found", HttpStatusCode.NotFound);
    }
}
