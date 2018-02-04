using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Models;

namespace SecretSanta.Service.IServices
{
    public interface IGroupService
    {

        Group GetGroupById(int groupId);
        IQueryable<Group> GetPageOfGroups(string userId, int take, int skip);
        IQueryable<ApplicationUser> GetMembers(int groupId);

        Task<Group> CreateGroupAsync(string groupName, string ownerId);
        Task RemoveUserFromGroup(int groupId, string userId);

    }
}
