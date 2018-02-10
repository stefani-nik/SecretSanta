namespace SecretSanta.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedsession : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        SessionId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        AuthToken = c.String(maxLength: 1024),
                        ExpirationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SessionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Sessions", new[] { "UserId" });
            DropTable("dbo.Sessions");
        }
    }
}
