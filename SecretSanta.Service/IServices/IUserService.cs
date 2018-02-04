using SecretSanta.Models;
using System.Linq;


namespace SecretSanta.Service.IServices
{
    public interface IUserService
    {
        ApplicationUser GetUserByUsername(string username);
        IQueryable<ApplicationUser> GetPageOfUsers(int page, int limit, bool sortAsc, string searchPattern);
        bool HasConnectedUsers(string userId, int groupId);

        void RegisterUser(ApplicationUser user);
        void LoginUser(string username, string password);


    }
}
