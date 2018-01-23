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

        public int Creator { get; set; }

        public virtual ICollection<string> Members { get; set; }

        public virtual ICollection<string> Invitations { get; set; }

        public virtual IDictionary<string,string> Connections { get; set; }

        public ConnectionsState State;


        public Group(string _name, string _creatorId)
        {
            this.Name = _name;
            this.CreatorId = _creatorId;
            this.Members = new HashSet<string>();
            this.Invitations = new HashSet<string>();
            this.Connections = new Dictionary<string, string>();
            this.State = ConnectionsState.NotConnected;
        }
    }
}
