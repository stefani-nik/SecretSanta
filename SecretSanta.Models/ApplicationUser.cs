using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SecretSanta.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string DisplayName { get; set; }

        public int ReceivedGifts { get; set; }

        public virtual ICollection<Group> CreatedGroups { get; set; }

        public virtual ICollection<Group> JoinedGroups { get; set; }

        public virtual ICollection<Invitation> PendingInvitations { get; set; }

        public ApplicationUser(string _username, string _displayName)
        {
            this.UserName = _username;
            this.DisplayName = _displayName;

            this.CreatedGroups = new HashSet<Group>();
            this.JoinedGroups = new HashSet<Group>();
            this.PendingInvitations = new HashSet<Invitation>();

            this.ReceivedGifts = 0;
        }

    }
}
