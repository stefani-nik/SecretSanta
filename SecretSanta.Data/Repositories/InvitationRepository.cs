using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Data.Infrastructure;
using SecretSanta.Data.IRepositories;
using SecretSanta.Models;

namespace SecretSanta.Data.Repositories
{
    public class InvitationRepository : RepositoryBase<Invitation>, IInvitationRepository
    {
        public InvitationRepository(SecretSantaContext dbContext) : base(dbContext) { }

        //private readonly IRepository<Invitation> repositoryBase;

        //public InvitationRepository(IRepository<Invitation> _repository)
        //{
        //    if (_repository == null)
        //    {
        //        throw new ArgumentException(nameof(_repository));

        //    }
        //    this.repositoryBase = _repository;
        //}

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

        public void ChangeInvitationState(Invitation invitation)
        {
            invitation.State = InvitationState.Accepted;
        }
    }
}
