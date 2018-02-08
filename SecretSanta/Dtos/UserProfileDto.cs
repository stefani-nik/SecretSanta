namespace SecretSanta.Dtos
{
    public class UserProfileDto
    {
        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public UserProfileDto(string username, string displayName)
        {
            this.UserName = username;
            this.DisplayName = displayName;
        }
    }
}