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

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public virtual ICollection<ApplicationUser> Members { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }

        public virtual IDictionary<ApplicationUser,ApplicationUser> Connections { get; set; }

        public ConnectionsState State;


        public Group(string _name, string _creatorId)
        {
            this.Name = _name;
            this.CreatorId = _creatorId;
            this.Members = new HashSet<ApplicationUser>();
            this.Invitations = new HashSet<Invitation>();
            this.Connections = new Dictionary<ApplicationUser, ApplicationUser>();
            this.State = ConnectionsState.NotConnected;
        }
    }
}
