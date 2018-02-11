using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SecretSanta.Data.Infrastructure;
using SecretSanta.Data.IRepositories;
using SecretSanta.Models;

namespace SecretSanta.Data.Repositories
{
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository(SecretSantaContext dbContext) : base(dbContext) { }

        public Group GetGroupById(int groupId)
        {
            return this.GetById(groupId);
        }

        public Group GetGroupByName(string name)
        {
            return this.GetAll.Include(g => g.Members).FirstOrDefault(g => g.Name == name);
        }

        public IEnumerable<Group> GetPageOfGroups(string username,int skip, int take)
        {
            var groups = this.GetAll.ToList();
            var result = groups.Where(g => g.Members.FirstOrDefault(m => m.UserName == username) != null).ToList();
            return result.OrderBy(g => g.Name).Skip(skip).Take(take);
        }

        public ICollection<ApplicationUser> GetMembers(int groupId)
        {
            return this.GetById(groupId).Members;

        }
    }

}