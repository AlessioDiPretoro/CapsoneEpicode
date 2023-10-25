namespace Stones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResponsePostDateActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostResponse", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.PostResponse", "date", c => c.DateTime(nullable: false));
            AddColumn("dbo.PostResponse", "dateEdit", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostResponse", "dateEdit");
            DropColumn("dbo.PostResponse", "date");
            DropColumn("dbo.PostResponse", "isActive");
        }
    }
}
