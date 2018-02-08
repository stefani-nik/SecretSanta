using System.ComponentModel.DataAnnotations;


namespace SecretSanta.Dtos
{
    public class CreateGroupDto
    {
        [Required]
        [MinLength(6)]
        public string Name { get; set; }
    }
}