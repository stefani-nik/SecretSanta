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
        private readonly IRepository<Group> repositoryBase;

        public GroupRepository(IRepository<Group> _repository)
        {
            if (_repository == null)
            {
                throw new ArgumentException(nameof(_repository));

            }
            this.repositoryBase = _repository;
        }

        public Group GetGroupById(int groupId)
        {
            return this.repositoryBase.GetById(groupId);
        }

        public IQueryable<Group> GetPageOfGroups(string userId,int skip, int take)
        {
            return this.repositoryBase.GetAll().Where(g => g.CreatorId == userId).Skip(skip).Take(take);
        }

        public IQueryable<ApplicationUser> GetMembers(int groupId)
        {
            var group = this.repositoryBase.GetById(groupId);
            return this.DbContext.Users.Where(u => u.JoinedGroups.Contains(group));

        }
    }

}