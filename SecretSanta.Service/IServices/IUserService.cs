using SecretSanta.Models;
using System.Linq;


namespace SecretSanta.Service.IServices
{
    public interface IUserService
    {
        ApplicationUser GetUserByUsername(string username);
        ApplicationUser GetUserById(string userId);
        IQueryable<ApplicationUser> GetPageOfUsers(int page, string orderBy, string searchPattern);
        bool HasConnectedUsers(string userId, int groupId);

    }
}
