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
            //Database.SetInitializer(new SeedData());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SecretSantaContext, Migrations.Configuration>());
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<Connection> Connections { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }

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
   

            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<TEntity> DbSet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }

        public void SetAdded<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Added;
        }

        public void SetDeleted<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public void SetUpdated<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Modified;
        }
    }

   
}