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
        private readonly IUnitOfWork _unitOfWork;

        public ConnectionService(IConnectionRepository connectionRepository, IUnitOfWork unitOfWork)
        {
            if (connectionRepository == null)
            {
                throw new ArgumentException(nameof(connectionRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentException(nameof(unitOfWork));
            }

            this._connectionRepository = connectionRepository;
            this._unitOfWork = unitOfWork;
        }

        public Connection GetConnectionInGroup(int groupId, string giverId)
        {
            return this._connectionRepository.GetConnectionInGroup(groupId, giverId);
        }

        public void CreateConnection(Connection connection)
        {
            this._connectionRepository.Add(connection);
            _unitOfWork.Commit();
        }
    }
}
