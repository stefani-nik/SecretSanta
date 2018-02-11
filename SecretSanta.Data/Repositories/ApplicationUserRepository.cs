using System;
using System.Collections.Generic;
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

        public ApplicationUser GetUserByUsername(string username)
        {
            return this.GetAll.FirstOrDefault(u => u.UserName == username);
        }

        public IEnumerable<ApplicationUser> GetPageOfUsers(int take, int skip, string orderBy, string searchPattern)
        {
            List<ApplicationUser> result = this.GetAll.ToList();
   

            if (!string.IsNullOrEmpty(searchPattern))
            {
                result = result
                    .Where(u => u.DisplayName.Contains(searchPattern) || u.UserName.Contains(searchPattern)).ToList();
            }

            switch (orderBy)
            {
                case "asc":
                    result = result
                        .OrderBy(u => u.DisplayName).ToList();
                    break;
                case "desc":
                    result = result
                        .OrderByDescending(u => u.DisplayName).ToList();
                    break;
            }

            result = result
                .Skip(skip)
                .Take(take)
                .ToList();

            return result;

        }

        public IEnumerable<Group> GetPageOfGroups(string username, int recordsOnPage, int skip)
        {
            return this.GetUserByUsername(username).JoinedGroups.Skip(skip).Take(recordsOnPage).ToList();
        }

    }
}
