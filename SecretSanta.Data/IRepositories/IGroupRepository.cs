using System.Linq;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Models;

namespace SecretSanta.Data.IRepositories
{
   public interface IGroupRepository : IRepository<Group>
    {
        Group GetGroupById(int groupId);
        Group GetGroupByName(string name);
        IQueryable<Group> GetPageOfGroups(string username, int take, int skip);
        IQueryable<ApplicationUser> GetMembers(int groupId);


    }
}
