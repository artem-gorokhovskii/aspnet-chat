using System.Threading.Tasks;

namespace chat.Services
{
    interface IAuthorizationService
    {
        public string GenerateHashFromPassword(string password);

        public Task<string> AsyncAuthenticate(string login, string password);
    }
}
