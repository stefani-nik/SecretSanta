namespace SecretSanta.Data.Configuration
{
    using System.Data.Entity.ModelConfiguration;
    using SecretSanta.Models;

    public class ConnectionsConfig : EntityTypeConfiguration<Connection>
    {
        public ConnectionsConfig()
        {
            ToTable("Connections");
            Property(c => c.ConnectionId).IsRequired();

            this.HasRequired(c => c.Group);

            this.HasRequired(c => c.Giver)
                .WithMany(u => u.UsersToGiveTo);

            this.HasRequired(c => c.Receiver);

        }
    }
}
