using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SecretSanta.Models
{
    public class User : IdentityUser
    {
        public User() { }

        public User(string username, string displayName) : this()
        {
            this.UserName = username;
            this.Email = "test.eme";

        }
    }
}