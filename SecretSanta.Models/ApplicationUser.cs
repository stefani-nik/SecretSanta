using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SecretSanta.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string DisplayName { get; set; }

        public int ReceivedGifts { get; set; }

        public virtual ICollection<string> CreatedGroups { get; set; }

        public virtual ICollection<string> JoinedGroups { get; set; }

        public virtual ICollection<string> PendingInvitations { get; set; }

        public ApplicationUser(string _username, string _displayName)
        {
            this.UserName = _username;
            this.DisplayName = _displayName;

            this.CreatedGroups = new HashSet<string>();
            this.JoinedGroups = new HashSet<string>();
            this.PendingInvitations = new HashSet<string>();

            this.ReceivedGifts = 0;
        }

    }
}
