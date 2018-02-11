using System.Collections.Generic;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Models;

namespace SecretSanta.Data.IRepositories
{
  public interface IApplicationUserRepository : IRepository<ApplicationUser>
  {
      ApplicationUser GetUserByUsername(string username);
      IEnumerable<ApplicationUser> GetPageOfUsers(int take, int skip, string orderBy, string searchPattern);
      IEnumerable<Group> GetPageOfGroups(string username, int recordsOnPage, int skip);
  }
}
