using System.Collections.Generic;
using SecretSanta.Models;

namespace SecretSanta.Service.IServices
{
    public interface IInvitationService
    {
        IEnumerable<Invitation> GetPageOfPendingInvitations(string userId,int page, string orderBy);
        void CreateInvittation(Invitation invitation);
        bool IsUserInvited(string groupName, string userId);

        void CancelInvitation(int groupId, string userId);
        void AcceptInvitation(string groupName, string userId);
    
    }
}
