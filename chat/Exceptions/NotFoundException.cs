using System.Net;

namespace chat.Exceptions
{
    public class NotFoundException : BasicException
    {
        public NotFoundException(string message) : base(message)
        {
            Status = HttpStatusCode.NotFound;
            Code = "NOT_FOUND";
        }
    }
}
