namespace Stones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoTitleInPosts : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Post", "title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Post", "title", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
