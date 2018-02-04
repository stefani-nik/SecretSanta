using System.Linq;
using SecretSanta.Models;

namespace SecretSanta.Service.IServices
{
    public interface IGroupService
    {

        Group GetGroupById(int groupId);
        IQueryable<Group> GetPageOfGroups(string userId, int page);
        IQueryable<ApplicationUser> GetMembers(int groupId);

        void CreateGroup(Group group);
        void RemoveUserFromGroup(int groupId, string userId);

    }
}
