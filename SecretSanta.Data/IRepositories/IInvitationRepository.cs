using System.Linq;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Models;

namespace SecretSanta.Data.IRepositories
{
    public interface IInvitationRepository : IRepository<Invitation>
    {
        IQueryable<Invitation> GetPageOfPendingInvitations(string userId, int take, int skip, string orderBy);

    }
}
