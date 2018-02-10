using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Data.Infrastructure;
using SecretSanta.Data.IRepositories;
using SecretSanta.Models;

namespace SecretSanta.Data.Repositories
{
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository(SecretSantaContext dbContext) : base(dbContext) { }

        //private readonly IRepository<Group> _repositoryBase;

        //public GroupRepository(IRepository<Group> repositoryBase)
        //{
        //    if (repositoryBase == null)
        //    {
        //        throw new ArgumentException(nameof(repositoryBase));

        //    }
        //    this._repositoryBase = repositoryBase;
        //}

        public Group GetGroupById(int groupId)
        {
            return this.GetById(groupId);
        }

        public Group GetGroupByName(string name)
        {
            return this.GetAll.FirstOrDefault(g => g.Name == name);
        }

        public IQueryable<Group> GetPageOfGroups(string username,int skip, int take)
        {
            return this.GetAll.Where(g => g.Creator.UserName == username).Skip(skip).Take(take);
        }

        public ICollection<ApplicationUser> GetMembers(int groupId)
        {
            return this.GetById(groupId).Members;
           // return this.DbContext.Users.Where(u => u.JoinedGroups.Contains(group));

        }
    }

}