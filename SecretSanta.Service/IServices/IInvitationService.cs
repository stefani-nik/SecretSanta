using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Models;

namespace SecretSanta.Service.IServices
{
    public interface IInvitationService
    {
        IQueryable<Invitation> GetPageOfPendingInvitations(string userId,int page, bool sortAsc);
        void CreateInvittation(Invitation invitation);
        bool IsUserInvited(int groupId, string userId);

        void CancelInvitation(int groupId, string userId);

        //   #TODO: Check if implementation of Accept Invitation is needed
    }
}
