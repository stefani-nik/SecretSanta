using SecretSanta.Data.IInfrastructure;
using SecretSanta.Models;

namespace SecretSanta.Data.IRepositories
{
    public interface IConnectionRepository : IRepository<Connection>
    {
        Connection GetConnectionInGroup(string username, string groupName);
    }
}
