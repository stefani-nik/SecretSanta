using System;

namespace SecretSanta.Dtos
{
    public class InvitationDto
    {

        public int Id { get; set; }

        public string GroupName { get; set; }

        public string OwnerName { get; set; }

        public DateTime Date { get; set; }

        public InvitationDto(int id, string groupName, string ownerName, DateTime date)
        {
            Id = id;
            GroupName = groupName;
            OwnerName = ownerName;
            Date = date;
        }
    }
}