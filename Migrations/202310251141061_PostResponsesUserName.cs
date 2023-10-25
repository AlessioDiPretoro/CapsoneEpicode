namespace Stones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostResponsesUserName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostResponse", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostResponse", "Username");
        }
    }
}
