
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

            this.HasMany(u => u.PendingInvitations)
                .WithRequired(i => i.Receiver)
                .WillCascadeOnDelete(false);

            this.HasMany(u => u.UsersToGiveTo)
                .WithRequired(c => c.Giver)
                .WillCascadeOnDelete(false);

            this.HasMany(u => u.UsersToGiveTo)
                .WithRequired(c => c.Receiver)
                .WillCascadeOnDelete(false);
        }
    }
}
