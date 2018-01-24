using System.Linq;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Models;

namespace SecretSanta.Data.IRepositories
{
   public interface IGroupRepository : IRepository<Group>
    {
        Group GetGroupById(int groupId);
        IQueryable<Group> GetPageOfGroups(string userId, int take, int skip);
        IQueryable<ApplicationUser> GetMembers(int groupId);


    }
}
