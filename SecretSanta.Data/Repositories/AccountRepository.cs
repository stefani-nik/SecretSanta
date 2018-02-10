using SecretSanta.Data.Infrastructure;
using SecretSanta.Data.IRepositories;
using SecretSanta.Models;

namespace SecretSanta.Data.Repositories
{
    public class AccountRepository : RepositoryBase<Session>, IAccountRepository
    {
        public AccountRepository(SecretSantaContext dbContext) : base(dbContext)
        {
        }
    }
}
