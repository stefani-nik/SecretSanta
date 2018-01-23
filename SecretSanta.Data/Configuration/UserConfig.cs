
namespace SecretSanta.Data.Configuration
{
    using System.Data.Entity.ModelConfiguration;
    using SecretSanta.Models;

    public class UserConfig : EntityTypeConfiguration<ApplicationUser>
    {
        public UserConfig()
        {
            ToTable("Users");
            Property(u => u.UserName).IsRequired().HasMaxLength(20);
            Property(u => u.DisplayName).IsRequired().HasMaxLength(30);

            this.HasMany(u=>u.CreatedGroups)
                .WithRequired(g => g.Creator)
                .WillCascadeOnDelete(true);

            this.HasMany(u => u.JoinedGroups)
                .WithMany(g => g.Members);

            this.HasMany(u => u.PendingInvitations)
                .WithRequired(i => i.Receiver)
                .WillCascadeOnDelete(true);

            this.HasMany(u => u.UsersToGiveTo)
                .WithRequired(c => c.Sender)
                .WillCascadeOnDelete(true);
        }
    }
}
