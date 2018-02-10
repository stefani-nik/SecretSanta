using System.Collections.Generic;
using System.Linq;
using SecretSanta.Models;

namespace SecretSanta.Service.IServices
{
    public interface IGroupService
    {

        Group GetGroupById(int groupId);
        Group GetGroupByName(string groupName);
        IQueryable<Group> GetPageOfGroups(string username, int page);
        ICollection<ApplicationUser> GetMembers(int groupId);

        void CreateGroup(Group group);
        void AddMember(string groupName, ApplicationUser user);
        void RemoveUserFromGroup(int groupId, string userId);
        void ChangeState(int groupId);

    }
}
