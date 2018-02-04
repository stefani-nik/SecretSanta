namespace SecretSanta.Models
{
    public class Connection
    {
        public int ConnectionId { get; set; }

        public int GroupId { get; set; }

        public virtual Group Group { get; set; }

        public string GiverId { get; set; }

        public virtual ApplicationUser Giver { get; set; }

        public string ReceiverId { get; set; }

        public virtual ApplicationUser Receiver { get; set; }

        public Connection(int _grouId, string _giverId, string _receiverId)
        {
            this.GroupId = _grouId;
            this.GiverId = _giverId;
            this.ReceiverId = _receiverId;
        }
    }
}
