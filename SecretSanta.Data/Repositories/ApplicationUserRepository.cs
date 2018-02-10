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
        public ApplicationUserRepository(SecretSantaContext dbContext) : base(dbContext) {}

        //private readonly IRepository<ApplicationUser> _repositoryBase;

        //public ApplicationUserRepository(IRepository<ApplicationUser> repositoryBase)
        //{
        //    if (repositoryBase == null)
        //    {
        //        throw new ArgumentException(nameof(repositoryBase));
        //    }

        //    this._repositoryBase = repositoryBase;
        //}

        public ApplicationUser GetUserByUsername(string username)
        {
            return this.GetAll.FirstOrDefault(u => u.UserName == username);
        }

        public IQueryable<ApplicationUser> GetPageOfUsers(int take, int skip, string orderBy, string searchPattern)
        {
            var result = this.GetAll;
   

            if (!string.IsNullOrEmpty(searchPattern))
            {
                result = result
                    .Where(u => u.DisplayName.Contains(searchPattern) || u.UserName.Contains(searchPattern));
            }

            if (orderBy == "asc")
            {
                result = result
                    .OrderBy(u => u.DisplayName);
            }
            else if(orderBy == "desc")
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
