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
        private readonly IRepository<Invitation> repositoryBase;

        public InvitationRepository(IRepository<Invitation> _repository)
        {
            if (_repository == null)
            {
                throw new ArgumentException(nameof(_repository));

            }
            this.repositoryBase = _repository;
        }

        public IQueryable<Invitation> GetPageOfPendingInvitations(string userId, int take, int skip, bool sortAsc)
        {
            var invitations = this.repositoryBase.GetAll()
                .Include(i => i.Group)
                .Where(i => i.State == InvitationState.Pending && i.Receiver.Id == userId);

            if (sortAsc)
            {
                invitations = invitations
                    .OrderBy(i => i.Date);
            }
            else
            {
                invitations = invitations
                   .OrderByDescending(i => i.Date);
            }

            invitations = invitations
                .Skip(skip)
                .Take(take);

            return invitations;
        }

    }
}
