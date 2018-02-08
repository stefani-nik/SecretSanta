using System.Threading.Tasks;
using SecretSanta.Models;

namespace SecretSanta.Service.IServices
{
    public interface IConnectionService
    {
        Connection GetConnectionInGroup(string username, string groupName);
        void CreateConnections(int groupId);
    }
}
