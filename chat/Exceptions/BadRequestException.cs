using System.Net;

namespace chat.Exceptions
{
    public class BadRequestException : BasicException
    {
        public BadRequestException(string message) : base(message)
        {
            Status = HttpStatusCode.BadRequest;
            Code = "BAD_REQUEST";
        }
    }
}
