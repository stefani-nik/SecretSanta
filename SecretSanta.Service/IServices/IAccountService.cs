using SecretSanta.Models;

namespace SecretSanta.Service.IServices
{
    public interface IAccountService
    {
        void CreateUserSession(string username, string authToken);

        bool InvalidateUserSession();

        Session ReValidateSession(string authToken);

        void DeleteExpiredSessions();
    }
}
