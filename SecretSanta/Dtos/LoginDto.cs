using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Dtos
{
    public class LoginDto
    {
        [Required]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}