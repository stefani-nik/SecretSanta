namespace SecretSanta.Data.Configuration
{
    using System.Data.Entity.ModelConfiguration;
    using SecretSanta.Models;

    public class GroupConfig : EntityTypeConfiguration<Group>
    {
        public GroupConfig()
        {
            ToTable("Groups");
            Property(g => g.Name).IsRequired().HasMaxLength(20);

            this.HasRequired(g => g.Creator)
                .WithMany(u => u.CreatedGroups);

            this.HasMany(g => g.Members)
                .WithMany(u => u.JoinedGroups)
                .Map(conf =>
                {
                    conf.ToTable("UserGroups")
                        .MapLeftKey("GroupId")
                        .MapRightKey("Id");
                });

            this.HasMany(g => g.Invitations)
                .WithRequired(i => i.Group)
                .WillCascadeOnDelete(true);


        }
    }
}
