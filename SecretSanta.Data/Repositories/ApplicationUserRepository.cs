using System;
using System.Linq;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Data.Infrastructure;
using SecretSanta.Data.IRepositories;
using SecretSanta.Models;

namespace SecretSanta.Data.Repositories
{
    public class ApplicationUserRepository : RepositoryBase<ApplicationUser>, IApplicationUserRepository
    {
        private readonly IRepository<ApplicationUser> repositoryBase;

        public ApplicationUserRepository(IRepository<ApplicationUser> _repository)
        {
            if (_repository == null)
            {
                throw new ArgumentException(nameof(_repository));
            }

            this.repositoryBase = _repository;
        }

        public ApplicationUser GetUserByUsername(string username)
        {
            return this.repositoryBase.GetAll().FirstOrDefault(u => u.UserName == username);
        }

        public IQueryable<ApplicationUser> GetPageOfUsers(int take, int skip, bool sortAsc, string searchPattern)
        {
            var result = this.repositoryBase.GetAll();
   

            if (!string.IsNullOrEmpty(searchPattern))
            {
                result = result
                    .Where(u => u.DisplayName.Contains(searchPattern) || u.UserName.Contains(searchPattern));
            }

            if (sortAsc)
            {
                result = result
                    .OrderBy(u => u.DisplayName);
            }
            else
            {
                result = result
                    .OrderByDescending(u => u.DisplayName);
            }

            result = result
                .Skip(skip)
                .Take(take);

            return result;

        }

        public bool HasConnectedUsers(string userId, int groupId)
        {
            return true;
        }
    }
}
