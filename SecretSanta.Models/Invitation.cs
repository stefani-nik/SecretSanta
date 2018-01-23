
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

        public string Group { get; set; }

        public string SenderId { get; set; }

        public string Sender { get; set; }

        public string ReceiverId { get; set; }

        public string Receiver { get; set; }

        public InvitationState State { get; set; }

        public DateTime Date { get; set; }

        public Invitation(int _groupId, string _senderId, string _receiverId, DateTime _date)
        {
            this.GropuId = _groupId;
            this.SenderId = _senderId;
            this.ReceiverId = _receiverId;
            this.Date = _date;
            this.State = InvitationState.Pending;
        }


    }
}
