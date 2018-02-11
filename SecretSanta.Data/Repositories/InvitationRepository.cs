using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SecretSanta.Data.Infrastructure;
using SecretSanta.Data.IRepositories;
using SecretSanta.Models;

namespace SecretSanta.Data.Repositories
{
    public class InvitationRepository : RepositoryBase<Invitation>, IInvitationRepository
    {
        public InvitationRepository(SecretSantaContext dbContext) : base(dbContext) { }

        public IEnumerable<Invitation> GetPageOfPendingInvitations(string userId, int take, int skip, string orderBy)
        {
            var invitations = this.GetAll
                .Include(i => i.Group)
                .Where(i => i.State == InvitationState.Pending && i.Receiver.Id == userId)
                .ToList();

            if (orderBy == "asc")
            {
                invitations = invitations
                    .OrderBy(i => i.Date).ToList();
            }
            else if(orderBy == "desc")
            {
                invitations = invitations
                   .OrderByDescending(i => i.Date).ToList();
            }

            invitations = invitations
                .Skip(skip)
                .Take(take)
                .ToList();

            return invitations;
        }

        public void AcceptInvitation(Invitation invitation)
        {
            invitation.State = InvitationState.Accepted;
        }

        public void DeclineInvitation(Invitation invitation)
        {
            invitation.State = InvitationState.Denied;
        }
    }
}
