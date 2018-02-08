using System.Collections.Generic;
using System.Linq;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Models;

namespace SecretSanta.Data.IRepositories
{
  public interface IApplicationUserRepository : IRepository<ApplicationUser>
  {
      ApplicationUser GetUserByUsername(string username);
      IQueryable<ApplicationUser> GetPageOfUsers(int take, int skip, string orderBy, string searchPattern);
      bool HasConnectedUsers(string userId, int groupId);
  }
}
