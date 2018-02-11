using System.Linq;
using SecretSanta.Data.Infrastructure;
using SecretSanta.Data.IRepositories;
using SecretSanta.Models;

namespace SecretSanta.Data.Repositories
{
    public class ConnectionRepository : RepositoryBase<Connection>, IConnectionRepository
    {
        public ConnectionRepository(SecretSantaContext dbContext) : base(dbContext) { }

        public Connection GetConnectionInGroup(string username, string groupName)
        {
            return this.GetAll.FirstOrDefault(c => c.Group.Name == groupName && c.Giver.UserName == username);
        }

    }
}
