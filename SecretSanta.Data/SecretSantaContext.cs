namespace SecretSanta.Data
{

    using System.Data.Entity;
    using SecretSanta.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SecretSanta.Data.Configuration;


    public class SecretSantaContext : IdentityDbContext<ApplicationUser>
    {
       
        public SecretSantaContext()
            : base("name=SecretSantaContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SecretSantaContext, Migrations.Configuration>());
        }

         public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<Connection> Connections { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        public static SecretSantaContext Create()
        {
            return new SecretSantaContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new GroupConfig());
            modelBuilder.Configurations.Add(new InvitationConfig());
            modelBuilder.Configurations.Add(new ConnectionsConfig());

            base.OnModelCreating(modelBuilder);
        }
    }

   
}