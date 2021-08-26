using System.Net;

namespace chat.Exceptions
{
    public class UnauthorizedException : BasicException
    {
        public UnauthorizedException(string message) : base(message)
        {
            Status = HttpStatusCode.Unauthorized;
            Code = "UNAUTHORIZED";
        }
    }
}
