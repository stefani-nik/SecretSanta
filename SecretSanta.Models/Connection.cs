namespace SecretSanta.Models
{
    public class Connection
    {
        public int ConnectionId { get; set; }

        public virtual Group Group { get; set; }

        public virtual ApplicationUser Giver { get; set; }

        public virtual ApplicationUser Receiver { get; set; }


    }
}
