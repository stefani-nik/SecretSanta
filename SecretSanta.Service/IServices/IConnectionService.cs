using System.Threading.Tasks;
using SecretSanta.Models;

namespace SecretSanta.Service.IServices
{
    public interface IConnectionService
    {
        Connection GetConnectionInGroup(int groupId, string giverId);
        void CreateConnection(Connection connection);
    }
}
