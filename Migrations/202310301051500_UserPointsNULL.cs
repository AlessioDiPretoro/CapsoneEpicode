namespace Stones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPointsNULL : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "points", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "points", c => c.Int(nullable: false));
        }
    }
}
