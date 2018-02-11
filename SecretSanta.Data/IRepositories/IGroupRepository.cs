using System.Collections.Generic;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Models;

namespace SecretSanta.Data.IRepositories
{
   public interface IGroupRepository : IRepository<Group>
    {
        Group GetGroupById(int groupId);
        Group GetGroupByName(string name);
        IEnumerable<Group> GetPageOfGroups(string username, int take, int skip);
        ICollection<ApplicationUser> GetMembers(int groupId);

    }
}
