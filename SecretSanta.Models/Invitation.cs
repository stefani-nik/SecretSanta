using System;

namespace SecretSanta.Models
{
     public enum  InvitationState
    {
        Pending,
        Accepted,
        Denied
    }
    public class Invitation
    {

        public int InvitationId { get; set; }

        public virtual Group Group { get; set; }


        public virtual ApplicationUser Receiver { get; set; }

        public InvitationState State { get; set; }

        public DateTime Date { get; set; }

        public Invitation()
        {
            this.State = InvitationState.Pending;
        }


    }
}
