namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SessionIsPublished : DbMigration
    {
        public override void Up()
        {
            AddColumn("session.Sessions", "IsPublished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("session.Sessions", "IsPublished");
        }
    }
}
