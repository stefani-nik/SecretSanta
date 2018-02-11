namespace SecretSanta.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ignoreusercolumns : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "EmailConfirmed");
            DropColumn("dbo.AspNetUsers", "SecurityStamp");
            DropColumn("dbo.AspNetUsers", "PhoneNumber");
            DropColumn("dbo.AspNetUsers", "PhoneNumberConfirmed");
            DropColumn("dbo.AspNetUsers", "TwoFactorEnabled");
            DropColumn("dbo.AspNetUsers", "AccessFailedCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String());
            AddColumn("dbo.AspNetUsers", "EmailConfirmed", c => c.Boolean(nullable: false));
        }
    }
}
