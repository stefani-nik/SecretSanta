using System.Collections.Generic;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Models;

namespace SecretSanta.Data.IRepositories
{
    public interface IInvitationRepository : IRepository<Invitation>
    {
        IEnumerable<Invitation> GetPageOfPendingInvitations(string userId, int take, int skip, string orderBy);
        void AcceptInvitation(Invitation invitation);
        void DeclineInvitation(Invitation invitation);
    }
}
