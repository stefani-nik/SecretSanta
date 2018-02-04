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
        IQueryable<Invitation> GetPageOfPendingInvitations(string userId, int take, int skip, bool sortAsc);
        Task<Invitation> CreateInvittationAsync(int groupId, string userId);
        bool IsUserInvited(int groupId, string userId);

        Task CancelInvitation(int groupId, string userId);
    }
}
