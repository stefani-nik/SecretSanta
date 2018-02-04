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
        private readonly IRepository<ApplicationUser> _repositoryBase;

        public ApplicationUserRepository(IRepository<ApplicationUser> repositoryBase)
        {
            if (repositoryBase == null)
            {
                throw new ArgumentException(nameof(repositoryBase));
            }

            this._repositoryBase = repositoryBase;
        }

        public ApplicationUser GetUserByUsername(string username)
        {
            return this._repositoryBase.GetAll().FirstOrDefault(u => u.UserName == username);
        }

        public IQueryable<ApplicationUser> GetPageOfUsers(int take, int skip, bool sortAsc, string searchPattern)
        {
            var result = this._repositoryBase.GetAll();
   

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
