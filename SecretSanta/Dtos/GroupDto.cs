using System.Collections.Generic;

namespace SecretSanta.Dtos
{
    public class GroupDto
    {
        public GroupDto(string groupName, string ownerName)
        {
            GroupName = groupName;
            CreatorName = ownerName;
        }

        public GroupDto(string groupName, string ownerName, ICollection<UserProfileDto> members)
            :this(groupName, ownerName)
        {
            Members = members;
        }

        public string GroupName { get; set; }

        public string CreatorName { get; set; }

        public ICollection<UserProfileDto> Members { get; set; }
    }
}