using System;

namespace SecretSanta.Dtos
{
    public class InvitationDto
    {

        public Guid Id { get; set; }

        public string GroupName { get; set; }

        public string OwnerName { get; set; }

        public DateTime Date { get; set; }

        public InvitationDto(Guid id, string groupName, string ownerName, DateTime date)
        {
            Id = id;
            GroupName = groupName;
            OwnerName = ownerName;
            Date = date;
        }
    }
}