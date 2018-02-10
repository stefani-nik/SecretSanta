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
        public ConnectionRepository(SecretSantaContext dbContext) : base(dbContext) { }

        //private readonly IRepository<Connection> repositoryBase;

        //public ConnectionRepository(IRepository<Connection> _repository)
        //{
        //    if (_repository == null)
        //    {
        //        throw  new ArgumentException(nameof(_repository));
        //    }

        //    this.repositoryBase = _repository;
        //}

        public Connection GetConnectionInGroup(string username, string groupName)
        {
            return this.GetAll.FirstOrDefault(c => c.Group.Name == groupName && c.Giver.UserName == username);
        }

    }
}
