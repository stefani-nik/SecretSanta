using System;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Models
{
    public class Session
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [MaxLength(1024)]
        public string AuthToken { get; set; }

        public DateTime ExpirationDateTime { get; set; }
    }
}
