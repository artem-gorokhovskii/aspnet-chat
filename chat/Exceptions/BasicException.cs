using System;
using System.Net;

namespace chat.Exceptions
{
    public class BasicException : Exception
    {
        public BasicException(string message) : base(message)
        {
            Status = HttpStatusCode.InternalServerError;
            Code = "UNHANDLED_EXCEPTION";
        }
        public HttpStatusCode Status { get; set; }

        public string Code { get; set; }
    }
}
