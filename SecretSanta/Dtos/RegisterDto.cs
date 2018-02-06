using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretSanta.Dtos
{
    public class RegisterDto
    {

        [Required]
        [StringLength(100, ErrorMessage = "The username must be at least {2} characters long.", MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The display name must be at least {2} characters long.", MinimumLength = 10)]
        public string DisplayName { get; set; }
    }
}