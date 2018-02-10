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
        IEnumerable<Invitation> GetPageOfPendingInvitations(string userId,int page, string orderBy);
        void CreateInvittation(Invitation invitation);
        bool IsUserInvited(string groupName, string userId);

        void CancelInvitation(int groupId, string userId);

        //   #TODO: Check if implementation of Accept Invitation is needed
    }
}
