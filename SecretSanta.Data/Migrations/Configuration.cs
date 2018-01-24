using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SecretSanta.Data.SecretSantaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SecretSanta.Data.SecretSantaContext";
        }
    }
}
