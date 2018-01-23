namespace SecretSanta.Data.Configuration
{
    using System.Data.Entity.ModelConfiguration;
    using SecretSanta.Models;

    public class InvitationConfig :EntityTypeConfiguration<Invitation>
    {
        public InvitationConfig()
        {
            ToTable("Invitations");
            Property(i => i.InvitationId).IsRequired();
            Property(i => i.Date).IsRequired();

            this.HasRequired(i => i.Group)
                .WithMany(g => g.Invitations);

            this.HasRequired(i => i.Receiver)
                .WithMany(u => u.PendingInvitations);

           
        }
    }
}
