using System.Collections.Generic;
using SecretSanta.Models;


namespace SecretSanta.Service.IServices
{
    public interface IUserService
    {
        ApplicationUser GetUserByUsername(string username);
        ApplicationUser GetUserById(string userId);
        IEnumerable<ApplicationUser> GetPageOfUsers(int page, string orderBy, string searchPattern);
        IEnumerable<Group> GetUserGroups(string username, int page);

    }
}
