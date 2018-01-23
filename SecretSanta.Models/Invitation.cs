
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
        
        public int GropuId { get; set; }

        public Group Group { get; set; }

        public string ReceiverId { get; set; }

        public ApplicationUser Receiver { get; set; }

        public InvitationState State { get; set; }

        public DateTime Date { get; set; }

        public Invitation(int _groupId, string _receiverId, DateTime _date)
        {
            this.GropuId = _groupId;
            this.ReceiverId = _receiverId;
            this.Date = _date;
            this.State = InvitationState.Pending;
        }


    }
}
