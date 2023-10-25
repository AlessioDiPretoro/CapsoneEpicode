namespace Stones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Post", "date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Post", "dateEdit", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Post", "dateEdit", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Post", "date", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
