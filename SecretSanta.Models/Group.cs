using System.Collections.Generic;

namespace SecretSanta.Models
{
    public enum ConnectionsState
    {
        Connected,
        NotConnected
    }
    public class Group
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<ApplicationUser> Members { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }

        public ConnectionsState State;

        public Group()
        {
            this.Members = new HashSet<ApplicationUser>();
            this.Invitations = new HashSet<Invitation>();
        }

        public Group(string name, ApplicationUser creator)
        {
            this.Name = name;
            this.Creator = creator;
            this.Members = new HashSet<ApplicationUser>();
            this.Invitations = new HashSet<Invitation>();
            this.State = ConnectionsState.NotConnected;
        }
    }
}
