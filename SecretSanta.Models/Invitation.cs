
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

        public Invitation(DateTime _date)
        {
           
            this.Date = _date;
            this.State = InvitationState.Pending;
        }


    }
}
