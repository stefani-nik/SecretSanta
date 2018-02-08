using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Data.IRepositories;
using SecretSanta.Models;
using SecretSanta.Service.IServices;

namespace SecretSanta.Service.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IConnectionRepository _connectionRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConnectionService(IConnectionRepository connectionRepository, IUnitOfWork unitOfWork, IGroupRepository groupRepository)
        {
            if (connectionRepository == null)
            {
                throw new ArgumentException(nameof(connectionRepository));
            }
            if (groupRepository == null)
            {
                throw new ArgumentException(nameof(groupRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentException(nameof(unitOfWork));
            }

            this._connectionRepository = connectionRepository;
            this._groupRepository = groupRepository;
            this._unitOfWork = unitOfWork;
        }

        public Connection GetConnectionInGroup(string username, string groupName)
        {
            return this._connectionRepository.GetConnectionInGroup(username, groupName);
        }

        private void CreateConnection(Connection connection)
        {
            this._connectionRepository.Add(connection);
            _unitOfWork.Commit();
        }

        public void CreateConnections(int groupId)
        {
            var memebers = this._groupRepository.GetMembers(groupId);
            var group = this._groupRepository.GetGroupById(groupId);
            var connections = this.GenerateConnections(memebers);

            foreach (var pair in connections)
            {
                var connection = new Connection
                {
                    Group = group,
                    Giver = memebers.ElementAt(pair.Key),
                    Receiver = memebers.ElementAt(pair.Value)
                };
                this.CreateConnection(connection);
            }
        }

        private IDictionary<int, int> GenerateConnections(IQueryable<ApplicationUser> members)
        {
            var count = members.Count();

            Dictionary<int, int> pairs = new Dictionary<int, int>();
            bool[] used = new bool[count];

            var rand = new Random();

            for (int i = 0; i < count; i++)
            {
                var connectToIndex = rand.Next(0, count);
                while (connectToIndex == i || used[connectToIndex])
                {
                    connectToIndex = rand.Next(0, count);
                }

                pairs[i] = connectToIndex;
                used[connectToIndex] = true;
            }

            return pairs;
        }
    }
}
