using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Data.Infrastructure;
using SecretSanta.Data.IRepositories;
using SecretSanta.Models;

namespace SecretSanta.Data.Repositories
{
    public class ConnectionRepository : RepositoryBase<Connection>, IConnectionRepository
    {
        private readonly IRepository<Connection> repositoryBase;

        public ConnectionRepository(IRepository<Connection> _repository)
        {
            if (_repository == null)
            {
                throw  new ArgumentException(nameof(_repository));
            }

            this.repositoryBase = _repository;
        }

        public Connection GetConnectionInGroup(int groupId, string userId)
        {
            return this.repositoryBase.GetAll().FirstOrDefault(c => c.GroupId == groupId && c.GiverId == userId);
        }

    }
}
