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
    public class InvitationService : IInvitationService
    {

        private readonly IInvitationRepository _invitationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InvitationService(IInvitationRepository invitationRepository, IUnitOfWork unitOfWork)
        {
            if (invitationRepository == null)
            {
                throw new ArgumentException(nameof(invitationRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentException(nameof(unitOfWork));
            }

            this._invitationRepository = invitationRepository;
            this._unitOfWork = unitOfWork;
        }


        public IQueryable<Invitation> GetPageOfPendingInvitations(string userId, int page , bool sortAsc = true)
        {
            const int recordsOnPage = 10;
            int skip = (page - 1) * recordsOnPage;
            return this._invitationRepository.GetPageOfPendingInvitations(userId, recordsOnPage, skip, sortAsc);
        }

        public void CreateInvittation(Invitation invitation)
        {   
            this._invitationRepository.Add(invitation);
            this._unitOfWork.Commit();
        }

        public bool IsUserInvited(int groupId, string userId)
        {
            var invitation = this._invitationRepository.GetAll()
                .FirstOrDefault(i => i.GropuId == groupId && i.ReceiverId == userId);

            return invitation != null;
        }

        public void CancelInvitation(int groupId, string userId)
        {
            var invitation = this._invitationRepository.GetAll()
                 .FirstOrDefault(i => i.GropuId == groupId && i.ReceiverId == userId);

            if (invitation == null)
            {
                return;
            }

            this._invitationRepository.Delete(invitation);
            this._unitOfWork.Commit();
           
        }
    }
}
